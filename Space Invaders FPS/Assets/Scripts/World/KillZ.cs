using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZ : MonoBehaviour
{
    public float killLimit = -100f;

    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if (playerTransform.position.y < killLimit)
        {
            GameManager.gameManager.ChangeLevel(2);
        }
    }
}
