using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour   
{
    [Header("Config")]
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float fallMultiplier;
    [SerializeField]
    private float jumpBuffer;
    [SerializeField]
    private float hangTime;

    [Header("References")]
    [SerializeField]
    private Transform foot;
    [SerializeField]
    private LayerMask groundLayers;

    private float jb;
    private float ht;

    private Rigidbody rb;
    private Vector3 spawn;

    private float mx;
    private float mz;

    private bool locked;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawn = transform.position;
    }

    void Update()
    {
        mx = Input.GetAxis("Horizontal");
        mz = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            jb = jumpBuffer;
        }
        else if (jb > 0)
        {
            jb -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (locked) return;

        // Handle hang-time (allow player to jump shortly after leaving the ground)
        if (Physics.CheckSphere(foot.position, .01f, groundLayers))
        {
            ht = hangTime;
        }
        else if (ht > 0)
        {
            ht -= Time.fixedDeltaTime;
        }

        // Jump
        if (jb > 0 && ht > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            jb = 0;
            ht = 0;
        }

        // Modify fall speed
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }

        // Update velocity with horizontal component
        rb.velocity = new Vector3(mx * movementSpeed, rb.velocity.y, mz * movementSpeed);

        // Cap horizontal movement speed (e.g. when moving diagonally)
        Vector2 v = new Vector2(rb.velocity.x, rb.velocity.z);
        if (v.magnitude > movementSpeed)
        {
            v.Normalize();
            v = v * movementSpeed;
            rb.velocity = new Vector3(v.x, rb.velocity.y, v.y);
        }
    }

    /// <summary>
	/// Respawn the player and enable movement
	/// </summary>
    public void Reset()
    {
        SetFriction(0f);
        rb.useGravity = true;
        transform.position = spawn;
        locked = false;
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
        rb.useGravity = false;
    }

    /// <summary>
	/// Change the player's friction
	/// </summary>
    public void SetFriction(float f)
    {
        Collider c = GetComponent<Collider>();
        c.material.dynamicFriction = f;
    }

    /// <summary>
	/// Smoothly move the player towards a target
    /// proportion defines the fraction of the distance that will be covered, and time defines how long it will take
	/// </summary>
    public IEnumerator MoveToTarget(Vector3 target, float proportion, float time)
    {
        float iterations = time / Time.fixedDeltaTime;
        float amount = 1 - Mathf.Pow(1 - proportion, 1 / iterations);

        for (int i = 0; i < iterations; i++)
        {
            rb.position = Vector3.Lerp(rb.position, target, amount);
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }
}
