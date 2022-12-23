using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour   
{
    [Header("Config")]
    [SerializeField]
    private int movementSpeed;
    public int MovementSpeed => movementSpeed;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float mx = Input.GetAxis("Horizontal");
        float mz = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(mx * movementSpeed, 0, mz * movementSpeed);
    }
}
