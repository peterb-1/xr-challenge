using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        col.transform.SetParent(transform);
    }

    void OnTriggerExit(Collider col)
    {
        col.transform.SetParent(null);
    }
}
