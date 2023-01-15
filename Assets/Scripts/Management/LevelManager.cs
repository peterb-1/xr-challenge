using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private GameObject exitPrefab;

    private Player player;
    private PlayerHitbox playerHitbox;

    private GameObject exit;
    public Transform exitLocation { get; private set; }

    private int currentLevel;

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
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        currentLevel = 0;
        AudioManager.instance.Play("BGM");
        SceneManager.sceneLoaded += OnSceneLoaded;

        Init();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Init();
    }

    /// <summary>
	/// Initialise the settings for the current level
	/// </summary>
    private void Init()
    {
        player = FindObjectOfType<Player>();
        playerHitbox = player.GetComponent<PlayerHitbox>();
        playerHitbox.OnPickUp += SpawnExit;

        exitLocation = GameObject.FindWithTag("ExitLocation").transform;
    }

    /// <summary>
	/// Move to the next level
	/// </summary>
    public IEnumerator NextLevel()
    {
        AudioManager.instance.Play("Click");
        AudioManager.instance.LowPass(22000f, 1f);

        yield return new WaitForSeconds(.5f);

        currentLevel++;
        SceneManager.LoadSceneAsync(currentLevel);
    }

    /// <summary>
	/// Restart the current level
	/// </summary>
    public IEnumerator Reset()
    {
        AudioManager.instance.Play("Click");
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

        AudioManager.instance.Play("ExitSpawn");
        exit = Instantiate(exitPrefab, exitLocation, false);
    }
}
