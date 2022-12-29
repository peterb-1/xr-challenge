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
        if (score > -1) OnPickUp?.Invoke(score);
    }

    private void HandleDeath()
    {
        Debug.Log("Initiating death sequence...");
        OnDeath?.Invoke();
    }

    private void HandleExit()
    {
        OnExit?.Invoke();
    }
}
