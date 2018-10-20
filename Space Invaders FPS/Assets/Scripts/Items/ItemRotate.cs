using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotate : MonoBehaviour
{
	private void Update ()
    {
        transform.Rotate(0.25f, 0.25f, 0.25f);
	}
}
