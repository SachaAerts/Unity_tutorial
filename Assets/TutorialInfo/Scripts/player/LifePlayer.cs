using TMPro;
using UnityEngine;

public class LifePlayer : MonoBehaviour
{
    public static LifePlayer Instance { get; private set; }
    
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI lifeText;
    
    [Header("Settings")]
    [SerializeField] private int maxLife = 3;

    private static int life = 3;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        life = maxLife;
        UpdateLifeDisplay();
    }

    void Update()
    {
        UpdateLifeDisplay();
        
        if (IsPlayerDead())
        {
            HandlePlayerDeath();
        }
    }

    private void UpdateLifeDisplay()
    {
        if (lifeText != null)
        {
            lifeText.text = $"Vies: {life}";
        }
    }

    public static void TakeDamages()
    {
        life--;
        
        if (Instance != null)
        {
            Instance.UpdateLifeDisplay();
        }
    }

    public static bool IsPlayerDead()
    {
        return life <= 0;
    }

    private void HandlePlayerDeath()
    {
        if (GameManager.Instance != null)
        {
            Cursor.visible = true;
            GameManager.Instance.EndGame();
        }
    }

    public static void ResetLife()
    {
        if (Instance != null)
        {
            life = Instance.maxLife;
        }
        else
        {
            life = 3;
        }
    }
}