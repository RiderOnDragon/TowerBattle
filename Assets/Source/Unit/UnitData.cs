using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Unit", fileName = "UnitData")]
public class UnitData : ScriptableObject
{
    [SerializeField] private Unit _prefab;

    [Space(10)]

    [SerializeField] private UnitTypeEnum _type;
    [SerializeField] private UnitTypeEnum _strength;
    [SerializeField] private UnitTypeEnum _weakness;

    [Space(5)]

    [SerializeField] private float _hp;
    [SerializeField] private float _towerDamage;
    [SerializeField] private float _unitDamage;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _moveSpeed;


    public const float WEAKNESS_DAMAGE_MULTIPLIER = 1.5f;
    public const float SUPERIORITY_DAMAGE_MULTIPLIER = 0.75f;

    public Unit Prefab { get => _prefab; }
    public UnitTypeEnum UnitType { get => _type; }
    public UnitTypeEnum Weakness { get => _weakness; }
    public UnitTypeEnum Superiority { get => _strength; }
    public float Hp { get => _hp; }
    public float TowerDamage { get => _towerDamage; }
    public float UnitDamage { get => _unitDamage; }
    public float AttackCooldown { get => _attackCooldown; }
    public float MoveSpeed { get => _moveSpeed; }
}
