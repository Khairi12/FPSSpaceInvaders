using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float backwalkSpeed = 0.5f;
    public float diagonalSpeed = 0.725f;

    public float rotateSpeed = 10.0f;
    public float rotateYMin = -60f;
    public float rotateYMax = 60f;

    public float jumpHeight = 5f;

    private Transform cameraTransform;
    private Transform playerTransform;
    private Rigidbody playerRigidbody;
    private Vector3 movementVector;
    private Vector3 forwardVelocity;
    private Vector3 horizontalVelocity;
    private Vector3 outgoingVelocity;

    private float outgoingSpeed = 0f;
    private float deltaRotateX = 0f;
    private float deltaRotateY = 0f;

    private void Start()
    {
        cameraTransform = GetComponentInChildren<Camera>().GetComponent<Transform>();
        playerTransform = GetComponent<Transform>();
        playerRigidbody = GetComponent<Rigidbody>();
        outgoingSpeed = moveSpeed;
    }

    private void DiagonalWalk()
    {
        if (Input.GetAxisRaw("Vertical") != 0f && Input.GetAxisRaw("Horizontal") != 0f)
            outgoingVelocity *= diagonalSpeed;
    }

    private void BackWalk()
    {
        if (Input.GetAxisRaw("Vertical") < 0f)
            outgoingVelocity *= backwalkSpeed;
    }

    private void Move()
    {
        forwardVelocity = transform.forward * Input.GetAxisRaw("Vertical");
        horizontalVelocity = transform.right * Input.GetAxisRaw("Horizontal");

        outgoingVelocity = forwardVelocity + horizontalVelocity;
        outgoingSpeed = moveSpeed;

        DiagonalWalk();
        BackWalk();

        playerRigidbody.velocity = new Vector3(
            outgoingVelocity.x * outgoingSpeed,
            playerRigidbody.velocity.y,
            outgoingVelocity.z * outgoingSpeed);
    }

    private void Jump()
    {
        playerRigidbody.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
    }

    private void Rotate()
    {
        deltaRotateX = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        deltaRotateY -= Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

        if (deltaRotateY < rotateYMin)
            deltaRotateY = rotateYMin;
        else if (deltaRotateY > rotateYMax)
            deltaRotateY = rotateYMax;

        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(new Vector3(0f, deltaRotateX, 0f)));
        cameraTransform.eulerAngles = new Vector3(deltaRotateY, playerTransform.localEulerAngles.y, playerTransform.localEulerAngles.z);
    }

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void FixedUpdate()
    {
        Rotate();
    }
}
