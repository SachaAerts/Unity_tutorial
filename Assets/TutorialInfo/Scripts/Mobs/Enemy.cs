using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;

    private readonly float minSpeed = 2f;

    private readonly float maxSpeed = 4f;

    private readonly int minHealth = 50;

    private readonly int maxHealth = 150;

    private EnemyStatistics ennemyStatistics;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        InitiateStats();
        gameObject.tag = "Ennemy";
    }

    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.position, ennemyStatistics.Speed * Time.deltaTime);
    }

    public void TakeDamages(int playerDamages)
    {
        ennemyStatistics.TakeDamages(playerDamages);
    }

    public bool IsPlayerKilledEnemy()
    {
        if (ennemyStatistics.IsEnemyDead())
        {
            Destroy(gameObject);
            return true;
        }

        return false;
    }

    private void InitiateStats()
    {
        ennemyStatistics = new()
        {
            Speed = Random.Range(minSpeed, maxSpeed),
            MaxHealth = Random.Range(minHealth, maxHealth),
            Health = maxHealth
        };
    }
}
