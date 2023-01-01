using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private Animator deathAnim;
    [SerializeField]
    private Animator finishAnim;

    [SerializeField]
    private GameObject gameplayFilter;
    [SerializeField]
    private Animator UIFilterAnim;

    private int score;

    private void Start()
    {
        score = 0;

        PlayerHitbox ph = FindObjectOfType<PlayerHitbox>();
        ph.OnPickUp += UpdateScore;
        ph.OnDeath += ShowDeathUI;
        ph.OnExit += ShowEndUI;
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
        deathAnim.SetBool("active", true);
        gameplayFilter.SetActive(true);
    }

    /// <summary>
	/// Shows the UI on completing the level
	/// </summary>
    private void ShowEndUI()
    {
        finishAnim.SetBool("active", true);
        gameplayFilter.SetActive(true);
    }

    /// <summary>
	/// Moves to the next level
	/// </summary>
    public void NextLevel()
    {
        StartCoroutine(HideUI(true));
        StartCoroutine(LevelManager.instance.NextLevel());
    }

    /// <summary>
	/// Restarts the current level
	/// </summary>
    public void Reset()
    {
        StartCoroutine(HideUI(false));
        StartCoroutine(LevelManager.instance.Reset());
    }

    /// <summary>
	/// Hides the death/level-end UI
	/// </summary>
    IEnumerator HideUI(bool nextLevel)
    {
        UIFilterAnim.SetBool("active", true);

        deathAnim.SetBool("active", false);
        finishAnim.SetBool("active", false);

        yield return new WaitForSeconds(.5f);

        gameplayFilter.SetActive(false);
        if (!nextLevel) UIFilterAnim.SetBool("active", false);

        score = 0;
        scoreText.text = "SCORE: 0";
    }
}
