using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    public Vector3 offset;

    [Header("References")]
    [SerializeField]
    private Transform player;

    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z), ref velocity, .1f);
    }
}
