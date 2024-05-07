using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiseUnitPanel : MonoBehaviour
{
    [SerializeField] private ChoiseUnitButton _choiseUnitButtonPrefab;

    [SerializeField] private Transform _buttonContainer;

    public void Init(PlayerData playerData)
    {
        foreach (var unit in playerData.AvailableUnits)
        {
            var button = Instantiate(_choiseUnitButtonPrefab, _buttonContainer);
            button.Init(unit, playerData.SpawnDelay);
        }
    }
}
