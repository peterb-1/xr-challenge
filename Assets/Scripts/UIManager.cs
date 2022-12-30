using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private GameObject deathText;
    [SerializeField]
    private GameObject finishText;
    [SerializeField]
    private GameObject filter;
    [SerializeField]
    private PlayerHitbox player;

    private int score;

    private void Start()
    {
        score = 0;
        player.OnPickUp += UpdateScore;
        player.OnDeath += ShowDeathUI;
        player.OnExit += ShowEndUI;
    }

    /// <summary>
	/// Displays the updated score on the UI
	/// </summary>
    private void UpdateScore(int increment)
    {
        score += increment;
        scoreText.text = "SCORE: " + score;
    }

    /// <summary>
	/// Shows the UI on player death
	/// </summary>
    private void ShowDeathUI()
    {
        deathText.SetActive(true);
        filter.SetActive(true);
    }

    /// <summary>
	/// Shows the UI on completing the level
	/// </summary>
    private void ShowEndUI()
    {
        finishText.SetActive(true);
        filter.SetActive(true);
    }
}
