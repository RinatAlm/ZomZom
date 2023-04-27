using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class EnemySpawnManager : MonoBehaviour
{
    public Transform player;
    public int numberOfEnemiesToSpawn;
    public float spawnDelay;
    public List<Enemy> enemyPrefabs = new List<Enemy>();
    public SpawnMethod EnemySpawnMethod = SpawnMethod.RoundRobin;
    NavMeshTriangulation triangulation;
    public bool isSpawnCoroutineRun = false;
    private Dictionary<int, ObjectPool> enemyObjectPools = new Dictionary<int, ObjectPool>();
    public int spawnedEnemies = 0;


    private void Start()
    {
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            enemyObjectPools.Add(i, ObjectPool.CreateInstance(enemyPrefabs[i], numberOfEnemiesToSpawn));
        }
        triangulation = NavMesh.CalculateTriangulation();

        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            if (EnemySpawnMethod == SpawnMethod.RoundRobin)
            {
                SpawnRoundRobinEnemy(spawnedEnemies);
            }
            else if (EnemySpawnMethod == SpawnMethod.Random)
            {
                SpawnRandomEnemy();
            }
            spawnedEnemies++;
        }


    }

    private void Update()
    {
        if(!isSpawnCoroutineRun)
        {
            StartCoroutine(SpawnEnemies());
        }     
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }

    IEnumerator SpawnEnemies()
    {
        isSpawnCoroutineRun = true;
        WaitForSeconds wait = new WaitForSeconds(spawnDelay);
        while (spawnedEnemies < numberOfEnemiesToSpawn)
        {
            if (EnemySpawnMethod == SpawnMethod.RoundRobin)
            {
                SpawnRoundRobinEnemy(spawnedEnemies);
            }
            else if (EnemySpawnMethod == SpawnMethod.Random)
            {
                SpawnRandomEnemy();
            }
            spawnedEnemies++;
            yield return wait;
        }
        isSpawnCoroutineRun = false;

    }

    private void SpawnRoundRobinEnemy(int spawnedEnemies)
    {
        int spawnIndex = spawnedEnemies % enemyPrefabs.Count;
        DoSpawnEnemy(spawnIndex);
    }

    private  void SpawnRandomEnemy()
    {
        DoSpawnEnemy(Random.Range(0, enemyPrefabs.Count));
    }

    private void DoSpawnEnemy(int spawnIndex)
    {
    PoolableObject poolableObject = enemyObjectPools[spawnIndex].GetObject();
        if (poolableObject != null)
        {
            Enemy enemy = poolableObject.GetComponent<Enemy>();
           
            int VertexIndex = Random.Range(0, triangulation.vertices.Length);
            NavMeshHit Hit;
            if (NavMesh.SamplePosition(triangulation.vertices[VertexIndex],out Hit,2f,-1))
            {
                enemy.navComponent.Warp(Hit.position);
                enemy.enemyMovement.target = player;
                enemy.navComponent.enabled = true;
                enemy.enemyMovement.StartChasing();
            }
            else
            {
                Debug.LogWarning("Unable to place Agent on nav mesh");
            }
        }
        else
        {
            Debug.LogWarning("Error -> no poolable objects");
        }
    }

    public enum SpawnMethod
    {
    RoundRobin,
    Random
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


}
