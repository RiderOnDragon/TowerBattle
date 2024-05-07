using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaInitializer : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    [Header("UI Settings")]
    [SerializeField] private Interface _interface;

    [Space(20)]

    [Header("Towers Settings")]
    [SerializeField] private PlayerTowerData _playerTower;
    [SerializeField] private EnemyTowerData _enemyTower;
    [SerializeField] private EnemyWaves _enemyWaves;
    [SerializeField] private Transform _towerContainer;

    [SerializeField] private Transform _playerTowerPosition;
    [SerializeField] private Transform _enemyTowerPosition;

    private void Awake()
    {
        var playerTower = Instantiate(_playerTower.Prefab, _towerContainer);
        var enemyTower = Instantiate(_enemyTower.Prefab, _towerContainer);

        playerTower.transform.position = (Vector2)_playerTowerPosition.position;
        enemyTower.transform.position = (Vector2)_enemyTowerPosition.position;

        playerTower.Init(_playerTower);
        enemyTower.Init(_enemyTower);

        if (enemyTower is EnemyTower enemy)
            enemy.InitWaves(_enemyWaves);
        else
            throw new System.ArgumentException();

        _interface.Init(_playerData);

        Time.timeScale = 1;
    }
}
