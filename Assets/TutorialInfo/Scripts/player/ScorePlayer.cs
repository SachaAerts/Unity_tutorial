using UnityEngine;
using TMPro;

public class ScorePlayer : MonoBehaviour
{
    public static ScorePlayer Instance { get; private set; }
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private static int score = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        score = 0;
        UpdateScoreDisplay();
    }

    void Update()
    {
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }

    public static void IncrementScore()
    {
        score++;
        
        if (Instance != null)
        {
            Instance.UpdateScoreDisplay();
        }
    }

    public static void ResetScore()
    {
        score = 0;
        
        if (Instance != null)
        {
            Instance.UpdateScoreDisplay();
        }
    }

    public static int GetScore()
    {
        return score;
    }
}