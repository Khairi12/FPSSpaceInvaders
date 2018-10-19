using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOTeleport : MonoBehaviour
{
    [HideInInspector]
    public bool teleporting { get; private set; }

    public Material opaqueMaterial;
    public Material fadeMaterial;

    public float fadeSpeed = 0.5f;

    public float minTeleportInterval = 1f;
    public float maxTeleportInterval = 5f;

    public float minTeleportDelay = 1f;
    public float maxTeleportDelay = 5f;

    public float teleportRangeX = 10f;
    public float teleportRangeZ = 10f;

    private Vector3 startPosition;
    private Color origColor;

    private float outTeleportInterval = 1f;

    private void Awake()
    {
        teleporting = false;
        origColor = transform.GetChild(0).GetComponent<Renderer>().material.color;
    }

    private void Start()
    {
        startPosition = transform.position;

        transform.position = new Vector3(
            startPosition.x + Random.Range(-teleportRangeX, teleportRangeX),
            startPosition.y,
            startPosition.z + Random.Range(-teleportRangeZ, teleportRangeZ));

        outTeleportInterval = Random.Range(minTeleportInterval, maxTeleportInterval);
    }

    private IEnumerator FadeOut()
    {
        Renderer[] children = GetComponentsInChildren<Renderer>();
        float alpha = 1f;

        foreach (Renderer rend in children)
        {
            rend.material = fadeMaterial;
        }

        while (alpha > 0f)
        {
            alpha -= fadeSpeed * Time.deltaTime;

            foreach (Renderer rend in children)
            {
                rend.material.color = new Color(origColor.r, origColor.g, origColor.b, alpha);
            }

            yield return null;
        }

        foreach (Renderer rend in children)
        {
            rend.material.color = new Color(origColor.r, origColor.g, origColor.b, 0);
        }

        yield break;
    }

    private IEnumerator FadeIn()
    {
        Renderer[] children = GetComponentsInChildren<Renderer>();
        float alpha = 0f;

        while (alpha < origColor.a)
        {
            alpha += fadeSpeed * Time.deltaTime;

            foreach (Renderer rend in children)
            {
                rend.material.color = new Color(origColor.r, origColor.g, origColor.b, alpha);
            }

            yield return null;
        }

        foreach (Renderer rend in children)
        {
            rend.material = opaqueMaterial;
            rend.material.color = new Color(origColor.r, origColor.g, origColor.b, 255);
        }

        yield break;
    }

    private IEnumerator Teleport()
    {
        Vector3 newPosition;

        teleporting = true;

        yield return StartCoroutine(FadeOut());

        newPosition = new Vector3(
            startPosition.x + Random.Range(-teleportRangeX, teleportRangeX),
            transform.position.y,
            startPosition.z + Random.Range(-teleportRangeZ, teleportRangeZ));

        transform.SetPositionAndRotation(newPosition, Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(minTeleportDelay, maxTeleportDelay));

        yield return StartCoroutine(FadeIn());

        teleporting = false;

        yield break;
    }

    private void Update()
    {
        if (teleporting)
            return;

        if (outTeleportInterval <= 0f)
        {
            outTeleportInterval = Random.Range(minTeleportInterval, maxTeleportInterval);
            StartCoroutine(Teleport());
        }
        else
        {
            outTeleportInterval -= Time.deltaTime;
        }
    }
}
