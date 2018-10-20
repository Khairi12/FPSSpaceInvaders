using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharUtil : MonoBehaviour
{
    public static bool IsTargetWithinRange(Transform transform, Vector3 targetPos, float range)
    {
        if (Vector3.Distance(transform.position, targetPos) < range)
        {
            return true;
        }

        return false;
    }
}
