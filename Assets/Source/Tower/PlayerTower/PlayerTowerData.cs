using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tower/PlayerTowerData", fileName = "PlayerTowerData")]
public class PlayerTowerData : TowerData
{
    [SerializeField] private PlayerTower _prefab;

    public override SideEnum Side { get => SideEnum.PLAYER; }
    public override Tower Prefab { get => _prefab; }
}
