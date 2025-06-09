using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private float SpawnInterval = 5f;
    
    private float _spawnTimer = 0f;

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= SpawnInterval)
        {
            Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            _spawnTimer = 0f;
        }
    }
}
