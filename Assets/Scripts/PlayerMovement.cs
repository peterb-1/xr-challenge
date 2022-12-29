using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour   
{
    [Header("Config")]
    [SerializeField]
    private int movementSpeed;

    private Rigidbody rb;

    private float mx;
    private float mz;

    private bool locked;

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
        if (locked) return;

        rb.velocity = new Vector3(mx * movementSpeed, 0, mz * movementSpeed);

        // Cap movement speed (e.g. when moving diagonally)
        if (rb.velocity.magnitude > movementSpeed)
        {
            rb.velocity = movementSpeed * Vector3.Normalize(rb.velocity);
        }
    }

    /// <summary>
	/// Lock the player's movement (but does not cancel their momentum)
	/// </summary>
    public void Lock()
    {
        locked = true;
    }

    /// <summary>
	/// Stop the player's movement
	/// </summary>
    public void Stop()
    {
        rb.velocity = Vector3.zero;
    }

    /// <summary>
	/// Smoothly move the player towards the target
	/// </summary>
    public IEnumerator MoveToTarget(Vector3 target)
    {
        for (int i = 0; i < 60; i++)
        {
            rb.position = Vector3.Lerp(rb.position, target, 0.1f);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }
}
