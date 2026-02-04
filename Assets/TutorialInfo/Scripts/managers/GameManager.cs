using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("References")]
    [SerializeField] private GameObject mobsSpawner;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EndGame()
    {
        if (mobsSpawner != null)
        {
            Destroy(mobsSpawner);
        }
        
        ResetGame();
        
        SceneManager.LoadScene("MenuScene");
    }

    private void ResetGame()
    {
        ScorePlayer.ResetScore();
        LifePlayer.ResetLife();
        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ennemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}