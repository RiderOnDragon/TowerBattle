using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EnemyWaves", fileName = "EnemyWaves")]
public class EnemyWaves : ScriptableObject
{
    [SerializeField] private List<EnemyWave> _enemyWaves;
    [SerializeField] private float _spawnSpeed;
    [SerializeField] private float _delayBetweenWaves;

    public float DelayBetweenWaves { get => _delayBetweenWaves; }
    public float SpawnSpeed { get => _spawnSpeed; }
    public IList<EnemyWave> Waves { get => _enemyWaves.AsReadOnly(); }


    [System.Serializable]
    public struct EnemyWave
    {
        [SerializeField] private List<UnitData> _units;

        public IList<UnitData> Units { get => _units.AsReadOnly(); }
    }
}
