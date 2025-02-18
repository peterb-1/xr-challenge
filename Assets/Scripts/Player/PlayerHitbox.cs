using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    public event Action<int> OnPickUp;
    public event Action OnDeath;
    public event Action OnExit;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Pickup":
                HandlePickup(other);
                break;
            case "Death":
                HandleDeath();
                break;
            case "Exit":
                HandleExit();
                break;
        }
    }

    private void HandlePickup(Collider other)
    {
        int score = other.gameObject.GetComponent<Pickup>().GetPickedUp();
        if (score > -1)
        {
            AudioManager.instance.Play("Pickup");
            OnPickUp?.Invoke(score);
        }
    }

    private void HandleDeath()
    {
        if (!GetComponent<Player>().dead)
        {
            AudioManager.instance.Play("Death");
            AudioManager.instance.LowPass(500f, .2f);
            OnDeath?.Invoke();
        }
    }

    private void HandleExit()
    {
        AudioManager.instance.Play("Exit");
        AudioManager.instance.LowPass(500f, .5f);
        OnExit?.Invoke();
    }
}
