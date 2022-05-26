using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    WaveConfigSO currentWave;
    LevelManager levelManager;
    bool isLooping = true;
    bool isReloadingPowerUp = false;

    void Awake() 
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public void SetReloading(bool value)
    {
        isReloadingPowerUp = value;
    }

    public bool GetReloading()
    {
        return isReloadingPowerUp;
    }

    IEnumerator SpawnEnemyWaves()
    {
        foreach(WaveConfigSO waveConfig in waveConfigs)
        {
            currentWave = waveConfig;

            for(int i = 0; i < currentWave.GetEnemyCount(); i++)
            {
                Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetStartingWaypoint().position, Quaternion.Euler(0,0,180), transform);
                yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }

        isLooping = false;
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    public void CheckWinningCondition() 
    {
        if (!isLooping)
        {
            int remainingEnemies = FindObjectsOfType<Pathfinder>().Length;
            if(remainingEnemies == 0)
            {
                levelManager.LoadGameOver();
            }
        }
    }
}
