using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerData", fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private List<UnitData> _availableUnits;
    [SerializeField] private float _spawnDelay;

    public IList<UnitData> AvailableUnits { get => _availableUnits.AsReadOnly(); }
    public float SpawnDelay { get => _spawnDelay; }
}
