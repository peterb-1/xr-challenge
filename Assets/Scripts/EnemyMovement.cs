using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    [Tooltip("True for horizontal circular movement, false for linear movement")]
    private bool circular;
    [SerializeField]
    [Tooltip("Defines the circle centre (ignoring Y) or endpoint of the line accordingly. This object's transform defines either a point on the circle, or the start point of the line.")]
    private Vector3 other;
    [SerializeField]
    private float cycleTime;
    [SerializeField]
    private float cycleOffset;
    [SerializeField]
    [Tooltip("Has no effect with linear motion")]
    private bool clockwise;

    private Vector3 start;
    private float radius;

    void Start()
    {
        start = transform.position;
        if (circular) radius = (start - other).magnitude;
    }

    void FixedUpdate()
    {
        cycleOffset += Time.fixedDeltaTime / cycleTime;
        cycleOffset %= 1;

        if (circular) CircularUpdate();
        else LinearUpdate();
    }

    /// <summary>
	/// Calculate the new location for circular movement based on the current cycle offset
	/// </summary>
    private void CircularUpdate()
    {
        float theta = 2 * Mathf.PI * (clockwise ? 1 - cycleOffset : cycleOffset);
        transform.position = other + radius * new Vector3(Mathf.Cos(theta), start.y, Mathf.Sin(theta));
    }

    /// <summary>
	/// Calculate the new location for linear movement based on the current cycle offset
	/// </summary>
    private void LinearUpdate()
    {
        float t = 1 - Mathf.Abs(2 * cycleOffset - 1);
        transform.position = Vector3.Lerp(start, other, t);
    }
}
