using UnityEngine;
using System.Collections.Generic;
using Redcode.Pools;
using Unity.VisualScripting;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private int MaxSpawnCount = 10;
    [SerializeField] private float SpawnInterval = 3f;
    
    private int _currentSpawnCount = 0;
    private float _spawnTimer = 0f;

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_currentSpawnCount < MaxSpawnCount && _spawnTimer >= SpawnInterval)
        {
            Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            _spawnTimer = 0f;
        }
    }
}
