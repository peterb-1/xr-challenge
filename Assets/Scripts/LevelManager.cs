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
    private GameObject exitPrefab;
    [SerializeField]
    private Player player;
    [SerializeField]
    private PlayerHitbox playerHitbox;

    private GameObject exit;

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
        playerHitbox.OnPickUp += SpawnExit;
        AudioManager.instance.Play("BGM");
    }

    /// <summary>
	/// Restart the current level
	/// </summary>
    public IEnumerator Reset()
    {
        AudioManager.instance.LowPass(22000f, 1f);

        yield return new WaitForSeconds(.5f);

        player.Reset();

        Pickup[] pickups = FindObjectsOfType<Pickup>();
        foreach (Pickup p in pickups)
        {
            p.Init();
        }

        Destroy(exit);
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
        exit = Instantiate(exitPrefab, exitLocation, false);
    }
}
