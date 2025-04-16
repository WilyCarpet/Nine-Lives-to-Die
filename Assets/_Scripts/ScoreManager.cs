using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int score = 0;
    public event Action<int> OnScoreChanged;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void IncreaseScore(int points)
    {
        score += points;
        OnScoreChanged?.Invoke(score); // Notify listeners
    }

    public void DecresaeScore(int points)
    {
        score -= points;
        OnScoreChanged?.Invoke(score); // Notify listeners
    }
}
