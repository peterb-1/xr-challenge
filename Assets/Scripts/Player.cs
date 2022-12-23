using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour   
{
    [Header("Config")]
    [SerializeField]
    private int movementSpeed;

    private Rigidbody rb;

    private float mx;
    private float mz;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        mx = Input.GetAxis("Horizontal");
        mz = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(mx * movementSpeed, 0, mz * movementSpeed);

        // Cap movement speed (e.g. when moving diagonally)
        if (rb.velocity.magnitude > movementSpeed)
        {
            rb.velocity = movementSpeed * Vector3.Normalize(rb.velocity);
        }
    }
}
