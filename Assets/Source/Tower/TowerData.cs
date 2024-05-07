using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerData : ScriptableObject
{
    [SerializeField] private int _hp;

    public abstract Tower Prefab { get; }
    public int Hp { get => _hp; }
    public UnitTypeEnum UnitType { get => UnitTypeEnum.TOWER; }
    public abstract SideEnum Side { get; }
}
