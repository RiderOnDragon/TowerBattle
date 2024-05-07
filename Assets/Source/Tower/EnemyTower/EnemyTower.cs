using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(TowerAI))]
public class EnemyTower : Tower
{
    [SerializeField] private TowerAI _towerAI;

    public void InitWaves(EnemyWaves waves)
    {
        _towerAI.Init(waves);
    }
}