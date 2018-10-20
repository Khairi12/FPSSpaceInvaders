using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienMovement : MonoBehaviour
{
    public enum MoveState { Patrolling, Idling, Chasing }
    public MoveState moveState = MoveState.Idling;

    public float walkingSpeed = 1f;
    public float chasingSpeed = 2f;
    public float idleDuration = 3f;
    public float attackRange = 3f;
    public float patrolRange = 10f;
    public float visibleRange = 15f;

    private Animator anim;
    private NavMeshAgent navAgent;
    private Transform playerTransform;
    private Vector3 startPosition;
    private float outIdleDuration;

    public bool hasAction { get; private set; }
    public bool disabledMovement { get; private set; }

    public void ToggleMovement(bool setting)
    {
        disabledMovement = setting;
        navAgent.isStopped = !setting;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        hasAction = false;
        disabledMovement = false;
        startPosition = transform.position;
        outIdleDuration = idleDuration;

        StartIdling();
    }

    private void StartPatrol()
    {
        navAgent.isStopped = false;
        navAgent.speed = walkingSpeed;
        moveState = MoveState.Patrolling;
        anim.SetBool("Walking", true);

        GetPatrolDestination();
    }

    private void StartIdling()
    {
        navAgent.isStopped = true;
        outIdleDuration = idleDuration;
        moveState = MoveState.Idling;
        anim.SetBool("Walking", false);
    }

    private void StartChasing()
    {
        navAgent.isStopped = false;
        navAgent.speed = chasingSpeed;
        moveState = MoveState.Chasing;
        anim.SetBool("Walking", true);

        navAgent.SetDestination(playerTransform.position);
    }

    private IEnumerator StartAttack()
    {
        hasAction = true;
        navAgent.isStopped = true;
        anim.SetBool("Walking", false);

        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(2);
        if (disabledMovement)
            yield break;

        if (CharUtil.IsTargetWithinRange(transform, playerTransform.position, attackRange))
        {
            playerTransform.GetComponent<HealthManager>().TakeDamage(10f);
        }

        yield return new WaitForSeconds(2);
        if (disabledMovement)
            yield break;

        anim.SetBool("Walking", true);
        navAgent.isStopped = false;
        hasAction = false;

        yield break;
    }

    private void Patrol()
    {
        if (CharUtil.IsTargetWithinRange(transform, playerTransform.position, visibleRange))
        {
            StartChasing();
        }
        else if (CharUtil.IsTargetWithinRange(transform, playerTransform.position, 1f))
        {
            StartIdling();
        }
    }

    private void Idle()
    {
        if (CharUtil.IsTargetWithinRange(transform, playerTransform.position, visibleRange))
        {
            StartChasing();
        }
        else if (outIdleDuration <= 0f)
        {
            StartPatrol();
        }
        else
        {
            outIdleDuration -= Time.deltaTime;
        }
    }

    private void Chase()
    {
        if (CharUtil.IsTargetWithinRange(transform, playerTransform.position, visibleRange))
        {
            if (CharUtil.IsTargetWithinRange(transform, playerTransform.position, attackRange))
            {
                StartCoroutine(StartAttack());
            }
            else
            {
                navAgent.SetDestination(playerTransform.position);
            }
        }
        else
        {
            StartIdling();
        }
    }

    private void GetPatrolDestination()
    {
        navAgent.SetDestination(new Vector3(
            startPosition.x + UnityEngine.Random.Range(-patrolRange, patrolRange),
            startPosition.y,
            startPosition.z + UnityEngine.Random.Range(-patrolRange, patrolRange)));
    }

    private void Update()
    {
        if (hasAction || disabledMovement)
            return;

        // normal behaviour
        switch (moveState)
        {
            case MoveState.Chasing:
                Chase();
                break;
            case MoveState.Idling:
                Idle();
                break;
            case MoveState.Patrolling:
                Patrol();
                break;
            default:
                Idle();
                break;
        }
    }
}
