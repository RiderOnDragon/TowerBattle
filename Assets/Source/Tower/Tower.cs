using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine;

[RequireComponent(typeof(TowerAnimation), typeof(AttackTargetView))]
public abstract class Tower : MonoBehaviour, IAttackTarget
{
    [SerializeField] private TowerAnimation _animation;
    [SerializeField] private AttackTargetView _view;

    private TowerData _data;
    private float _currentHp;

    private const string DEATH_EVENT_NAME = "destroy";

    public event Action Destroyed;

    public void Init(TowerData data)
    {
        _data = data;
        _currentHp = _data.Hp;

        _animation.SkeletonAnimation.AnimationState.Event += DestroyTower;
    }

    private void OnDestroy()
    {
        _animation.SkeletonAnimation.AnimationState.Event -= DestroyTower;

        Destroyed?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException();

        damage = damage > _currentHp ? _currentHp : damage;

        _currentHp -= damage;

        _view.UpdateHp(_data.Hp, _currentHp);

        if (_currentHp == 0)
            _animation.StartDeath();
    }

    private void DestroyTower(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name != DEATH_EVENT_NAME)
            return;

        Destroy(gameObject);
    }

    public SideEnum GetSide()
    {
        return _data.Side;
    }

    public UnitTypeEnum GetUnitType()
    {
        return _data.UnitType;
    }
}
