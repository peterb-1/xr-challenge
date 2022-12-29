using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField]
    private Transform exitLocation;
    public Transform ExitLocation => exitLocation;

    [Header("References")]
    [SerializeField]
    private GameObject exit;
    [SerializeField]
    private PlayerHitbox player;

    public static LevelManager instance { get; private set; }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        player.OnPickUp += SpawnExit;
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
        Instantiate(exit, exitLocation, false);
    }
}
