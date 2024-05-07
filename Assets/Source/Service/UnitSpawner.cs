using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Transform _unitContainer;

    public static UnitSpawner Singleton;

    private void Awake()
    {
        if (Singleton == null)
            Singleton = this;
        else
            Destroy(gameObject);
    }

    public void SpawnUnit(UnitData unitData, Vector2 position, SideEnum side, Transform container = null)
    {
        Vector2 rotation = Vector2.zero;

        if (side == SideEnum.ENEMY)
            rotation = new Vector2(0, 180);

        var unit = Instantiate(unitData.Prefab, position, Quaternion.Euler(rotation));
        unit.Init(unitData, side);

        if (container != null) 
            unit.transform.SetParent(container);
        else
            unit.transform.SetParent(_unitContainer);
    }
}
