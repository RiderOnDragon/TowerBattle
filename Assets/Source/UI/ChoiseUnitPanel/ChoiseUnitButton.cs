using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(ChoiseUnitBottonView))]
public class ChoiseUnitButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private ChoiseUnitBottonView _view;

    private UnitData _unit;
    private float _spawnDelay;

    private Transform _playerTower;

    private static event Action UnitSpawned;

    public void Init(UnitData unit, float spawnDelay)
    {
        _unit = unit;
        _spawnDelay = spawnDelay;

        _playerTower = FindObjectOfType<PlayerTower>().transform;

        _button.onClick.AddListener(SpawnUnit);

        _view.Init(_unit);

        UnitSpawned += OnUnitSpawned;
    }

    private void OnDestroy()
    {
        UnitSpawned -= OnUnitSpawned;
    }

    private void SpawnUnit()
    {
        UnitSpawner.Singleton.SpawnUnit(_unit, _playerTower.position, SideEnum.PLAYER);

        UnitSpawned?.Invoke();
    }

    private void OnUnitSpawned()
    {
        _button.interactable = false;

        StartCoroutine(Waiting(_spawnDelay));
    }

    private IEnumerator Waiting(float time)
    {
        yield return new WaitForSeconds(time);

        _button.interactable = true;
    }
}
