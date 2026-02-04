using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class MobsSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private BoxCollider ground;
    [SerializeField] private GameObject enemyPrefab;

    private readonly float spawnInterval = 1f;
    private readonly int minDistanceFromPlayer = 7;

    private CancellationTokenSource cancellationTokenSource;

    void Start()
    {
        cancellationTokenSource = new CancellationTokenSource();
        _ = SpawnRoutine(cancellationTokenSource.Token);
    }

    void OnDestroy()
    {
        cancellationTokenSource?.Cancel();
        cancellationTokenSource?.Dispose();
    }

    private async Task SpawnRoutine(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay((int)(spawnInterval * 1000), cancellationToken);
            
            if (this == null || cancellationToken.IsCancellationRequested)
                break;
                
            SpawnMob();
        }
    }

    private void SpawnMob()
    {
        Bounds bounds = ground.bounds;

        float coordX = Random.Range(bounds.min.x, bounds.max.x);
        float coordZ = Random.Range(bounds.min.z, bounds.max.z);
        float coordY = bounds.max.y + 1f;

        Vector3 spawn = new(coordX, coordY, coordZ);

        if (Vector3.Distance(spawn, player.position) > minDistanceFromPlayer)
        {
            Instantiate(enemyPrefab, spawn, Quaternion.identity);
        }
    }
}