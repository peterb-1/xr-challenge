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
    private PlayerHitbox player;

    private int score;

    private void Start()
    {
        score = 0;
        player.OnPickUp += UpdateScore;
    }

    public void UpdateScore(int increment)
    {
        score += increment;
        scoreText.text = "SCORE: " + score;
    }
}
