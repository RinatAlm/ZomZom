using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
///  Script that spawns Enemies accross the map
/// </summary>

public class EnemySpawnManager : MonoBehaviour
{
    public Transform player;
    public List<Enemy> enemyPrefabs = new();
    [SerializeField]private int _numberOfEnemiesToSpawn;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _minRad = 2;
    [SerializeField] private float _maxRad = 5;
    [SerializeField] private float _sphereCheckRadius;
    private SpawnMethod _enemySpawnMethod = SpawnMethod.RoundRobin;  
    private bool _isSpawnCoroutineRun = false;
    private Dictionary<int, ObjectPool> _enemyObjectPools = new();
    private int _spawnedEnemies = 0;
    private Vector3 _spawnPosition;
    private Collider[] hitColliders;
    private int _findAwailablePosCounter;

    private void Start()
    {      
        PrespawnEnemies();
    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            if (!_isSpawnCoroutineRun)
            {
                StartCoroutine(SpawnEnemies());
            }          
        }
    }


    private void PrespawnEnemies()//Spawn enemies before usage
    {
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            _enemyObjectPools.Add(i, ObjectPool.CreateInstance(enemyPrefabs[i], _numberOfEnemiesToSpawn));
        }
    }
    private IEnumerator SpawnEnemies()
    {
        _isSpawnCoroutineRun = true;
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);
        while (_spawnedEnemies < _numberOfEnemiesToSpawn)
        {
            if (_enemySpawnMethod == SpawnMethod.RoundRobin)
            {
                SpawnRoundRobinEnemy(_spawnedEnemies);
            }
            else if (_enemySpawnMethod == SpawnMethod.Random)
            {
                SpawnRandomEnemy();
            }
            _spawnedEnemies++;
            yield return wait;
        }
        _isSpawnCoroutineRun = false;

    }

    private void SpawnRoundRobinEnemy(int spawnedEnemies)
    {
        int spawnIndex = spawnedEnemies % enemyPrefabs.Count;
        DoSpawnEnemy(spawnIndex);
    }

    private void SpawnRandomEnemy()
    {
        DoSpawnEnemy(UnityEngine.Random.Range(0, enemyPrefabs.Count));
    }

    private void DoSpawnEnemy(int spawnIndex)
    {
        PoolableObject poolableObject = _enemyObjectPools[spawnIndex].GetObject();
        if (poolableObject != null)
        {
            //int VertexIndex = Random.Range(0, triangulation.vertices.Length);
            //NavMeshHit Hit;
            //if (NavMesh.SamplePosition(triangulation.vertices[VertexIndex],out Hit,2f,-1))
            //{
            //    enemy.navComponent.Warp(Hit.position);
            //    enemy.enemyMovement.target = player;
            //    enemy.navComponent.enabled = true;
            //    enemy.enemyMovement.StartChasing();
            //}
            Enemy enemy = poolableObject.GetComponent<Enemy>();
            repeat:
            if(_findAwailablePosCounter>50)
            {
                Debug.LogAssertion("Position not found");
            }
            if (FindAvailablePosition())
            {
                enemy.transform.position = _spawnPosition;
                enemy.navComponent.enabled = true;
                enemy.enemyMovement.StartChasing();
            }
            else
            {
                goto repeat;
            }
        }
        else
        {
            Debug.LogWarning("Error -> no poolable objects");
        }
    }

    Vector3 CalculateSpawnOffset()
    {      
        Vector3 offset = UnityEngine.Random.onUnitSphere.normalized * UnityEngine.Random.Range(_minRad, _maxRad);
        return offset;
    }

   
    private bool FindAvailablePosition()
    {     
        _spawnPosition = player.position + CalculateSpawnOffset();
        _spawnPosition = new Vector3(_spawnPosition.x, 0, _spawnPosition.z);
        hitColliders = Physics.OverlapSphere(_spawnPosition, _sphereCheckRadius);
        NavMeshHit hit;
        _findAwailablePosCounter++;
        if (hitColliders.Length == 1 && NavMesh.SamplePosition(_spawnPosition, out hit, _sphereCheckRadius, NavMesh.AllAreas))
        {
            _findAwailablePosCounter = 0;
            return true;
        }
        else
        {
            return false;
        }
        
    }
    public enum SpawnMethod
    {
        RoundRobin,
        Random
    }

    public void DecrementEnemiesNumber()
    {
        _spawnedEnemies--;
    }

}
