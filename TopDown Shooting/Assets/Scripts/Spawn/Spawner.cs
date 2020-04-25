using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Wave[] waves;
    public Enemy enemy;

    Wave currentWave;
    int currentWaveNumber;

    int enemiesRemaingToSpawn; // 남아있는 스폰할 적;
    int enemiesRemaingLive; // 살아있는 적의 수
    float nextSpawnTime;

    void Start()
    {
        NextWave();
    }

    void Update()
    {
        if (enemiesRemaingToSpawn > 0 && Time.time > nextSpawnTime) {
            enemiesRemaingToSpawn--;
            nextSpawnTime = Time.time + currentWave.timeBetweenSpawn;

            Enemy spawnEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as Enemy;
            spawnEnemy.OnDeath += OnEnemyDeath;
        }
    }

    void OnEnemyDeath()
    {
        enemiesRemaingLive --;

        if(enemiesRemaingLive == 0)
        {
            NextWave();
        }
        print("Enemy Died!");
    }

    void NextWave()
    {
        currentWaveNumber++;
        Debug.Log("Wave : " + currentWaveNumber);
        if (currentWaveNumber - 1 < waves.Length) {
            currentWave = waves[currentWaveNumber - 1];

            enemiesRemaingToSpawn = currentWave.enemyCount;
            enemiesRemaingLive = enemiesRemaingToSpawn;
        }
    }

    [System.Serializable]
    public class Wave
    {
        public int enemyCount;
        public float timeBetweenSpawn;
    }
}
