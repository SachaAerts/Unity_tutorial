using NUnit.Framework.Internal;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Ennemy : MonoBehaviour
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
        
        if (ennemyStatistics.IsEnemyDead())
        {
            Destroy(gameObject);
        }
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
