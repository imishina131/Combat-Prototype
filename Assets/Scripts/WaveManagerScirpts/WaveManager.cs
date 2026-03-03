using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    // list shown in inspector, can manually add grunt, tank, boss, etc.
    public List<EnemyData> enemyTypes;

    // total budget for the wave
    public int wavePointCap = 20;
    public float timeBetweenWaves = 3f;

    private int currentWave = 0;
    private int aliveEnemies = 0;

    // When an enemy dies, call HandleEnemyKilled
    private void OnEnable()
    {
        Enemy.OnEnemyKilled += HandleEnemyKilled;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyKilled -= HandleEnemyKilled;
    }

    private void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        currentWave++;
        SpawnWave();
    }

    private void SpawnWave()
    {
        int pointsRemaining = wavePointCap;
        
        // keep spawning until no points remain
        while (pointsRemaining > 0)
        {
            // only picks enemies the wave can afford
            EnemyData selected = GetWeightedRandomEnemy(pointsRemaining);

            if (selected == null)
                break;

            SpawnEnemy(selected);
            // decreases budget
            pointsRemaining -= selected.pointCost;
        }
    }

    void SpawnEnemy(EnemyData data)
    {
        Vector3 randomPos = new Vector3(
            Random.Range(-10,10),
            0,
            Random.Range(-10,10)
        );

        Instantiate(data.prefab, randomPos, Quaternion.identity);
        aliveEnemies++;
    }

    EnemyData GetWeightedRandomEnemy(int pointsRemaining)
    {
        List<EnemyData> validEnemies = new List<EnemyData>();

        foreach (var enemy in enemyTypes)
        {
            if (enemy.pointCost <= pointsRemaining)
                validEnemies.Add(enemy);
        }

        if (validEnemies.Count == 0)
            return null;

        int totalWeight = 0;
        foreach (var enemy in validEnemies)
        {
            totalWeight += enemy.weight;
        }

        int randomValue = Random.Range(0, totalWeight);

        int cumulative = 0;
        foreach (var enemy in validEnemies)
        {
            cumulative += enemy.weight;
            if (randomValue < cumulative)
                return enemy;
        }

        return null;
    }

    void HandleEnemyKilled(Enemy enemy)
    {
        aliveEnemies--;

        if (aliveEnemies <= 0)
        {
            StartCoroutine(StartNextWave());
        }
    }
}
