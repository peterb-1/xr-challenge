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
        player.OnExit += FinishLevel;
    }

    /// <summary>
	/// Makes the exit appear in the level if all pickups have been collected
	/// </summary>
    private void SpawnExit(int placeholder)
    {
        Pickup[] pickups = FindObjectsOfType<Pickup>();
        foreach (Pickup p in pickups)
        {
            if (!p.IsCollected) return;
        }
        Instantiate(exit, location, false);
    }

    /// <summary>
	/// Initiate the end-of-level process
	/// </summary>
    private void FinishLevel()
    {
        Debug.Log("Finishing the level...");
    }
}
