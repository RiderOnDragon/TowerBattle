using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAI : MonoBehaviour
{
    private EnemyWaves _enemyWaves;

    private int _currentWave;

    public void Init(EnemyWaves enemyWaves)
    {
        _enemyWaves = enemyWaves;

        _currentWave = 0;

        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0 ; i < _enemyWaves.Waves.Count; i++)
        {
            yield return new WaitForSeconds(_enemyWaves.DelayBetweenWaves);

            for (int j = 0; j < _enemyWaves.Waves[_currentWave].Units.Count; j++)
            {
                UnitSpawner.Singleton.SpawnUnit(_enemyWaves.Waves[_currentWave].Units[j], transform.position, SideEnum.ENEMY);
                yield return new WaitForSeconds(_enemyWaves.SpawnSpeed);
            }

            _currentWave++;
        }
    }
}
