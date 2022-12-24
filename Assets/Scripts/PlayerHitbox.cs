using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{

    public event Action<int> OnPickUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            HandlePickup(other);
        }
    }

    private void HandlePickup(Collider other)
    {
        int score = other.gameObject.GetComponent<Pickup>().GetPickedUp();
        if (score > -1) OnPickUp?.Invoke(score);
    }
}
