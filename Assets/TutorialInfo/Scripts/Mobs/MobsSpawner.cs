using System.Collections;
using UnityEngine;

public class MobsSpawner: MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private BoxCollider ground;

    [SerializeField] private GameObject enemyPrefab;

    private readonly float spawnInterval = 1f;

    private readonly int minDistance = 7;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnInterval);
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

        if(Vector3.Distance(spawn, player.position) > minDistance)
        {
            Instantiate(enemyPrefab, spawn, Quaternion.identity);
        }
    }
}