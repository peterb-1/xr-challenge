using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private PlayerAnimator animator;
    [SerializeField]
    private PlayerHitbox hitbox;
    [SerializeField]
    private PlayerMovement movement;

    private bool dead;

    void Start()
    {
        hitbox.OnDeath += Die;
        hitbox.OnExit += EndLevel;
    }

    /// <summary>
	///Play the death effect, and lock and completely stop the player's movement as they die
	/// </summary>
    private void Die()
    {
        if (dead) return;

        dead = true;
        animator.PlayDeath();
        movement.Lock();
        movement.Stop();
    }

    /// <summary>
	/// Lock the player's movement at the end of the level and center them on the exit
	/// </summary>
    private void EndLevel()
    {
        movement.Lock();
        StartCoroutine(movement.MoveToTarget(LevelManager.instance.ExitLocation.position));
    }
}
