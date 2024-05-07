using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tower/EnemyTowerData", fileName = "EnemyTowerData")]
public class EnemyTowerData : TowerData
{
    [SerializeField] private EnemyTower _prefab;

    public override Tower Prefab { get => _prefab; }
    public override SideEnum Side { get => SideEnum.ENEMY; }
}
