using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Renderer modelRenderer;

    [Header("Effects")]
    [SerializeField]
    private ParticleSystem deathEffect;

    /// <summary>
	/// Play the death animation and effects.
	/// </summary>
	public void PlayDeath()
    {
        enabled = false;
        modelRenderer.enabled = false;
        deathEffect.Play();
    }
}
