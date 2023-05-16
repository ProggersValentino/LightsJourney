using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawnMan : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public float spawnInterval = 2f;
    public int maxEnemies; //capping on how many can spawn in one manager
    public Transform[] spawnPoints;
    public bool isSpawning = true;

        void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        IEnumerator SpawnEnemies()
        {
            while (isSpawning)
            {
                if (GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
                {
                    int rand = Random.Range(0, enemyPrefab.Length); //cycles through randomly spawning an enemy
                    GameObject enemyToSpawn = enemyPrefab[rand];
                    
                    Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; //chooses a random point within the list to spawn enemy
                    Instantiate(enemyToSpawn, randomSpawnPoint.position, Quaternion.identity);
                }

                yield return new WaitForSeconds(spawnInterval);
            }
        }
}

///
/// logic
/// gameobnject that has coord of map boundaries
/// instantiate spawner object (script attached) in a random location
/// get players transform and have circle
///
/// vector3(player.x, y, player.z
/// xpint = random.range(3, 20)
/// zpint = random.range(3, 20)
/// 
