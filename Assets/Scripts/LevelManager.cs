using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private Transform location;

    [Header("References")]
    [SerializeField]
    private GameObject exit;
    [SerializeField]
    private PlayerHitbox player;

    void Start()
    {
        player.OnPickUp += SpawnExit;
    }

    private void SpawnExit(int placeholder)
    {
        Pickup[] pickups = FindObjectsOfType<Pickup>();
        foreach (Pickup p in pickups)
        {
            if (!p.IsCollected) return;
        }
        Debug.Log("Trying to create exit...");
        Instantiate(exit, location, false);
    }
}
