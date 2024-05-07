using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine;

[RequireComponent(typeof(Rigidbody2D), typeof(UnitAnimation), typeof(AttackTargetView))]
public class Unit : MonoBehaviour, IAttackTarget
{
    [SerializeField] private UnitAnimation _animation;
    [SerializeField] private AttackTargetView _view;

    [SerializeField] private CircleCollider2D _attackRadius;
    [SerializeField] private CircleCollider2D _stopDistanceRadius;

    private UnitData _data;
    private float _currentHp;

    private SideEnum _side;

    private StateMashine _stateMashine;

    private const float _fov = 180;

    private const string DEATH_EVENT_NAME = "destroy";

    public UnitData Data { get => _data; }
    public UnitAnimation Animation { get => _animation; }
    public SideEnum UnitSide { get => _side; }

    public event Action Destroyed;

    public void Init(UnitData data, SideEnum side)
    {
        _data = data;
        _currentHp = _data.Hp;

        _side = side;

        _animation.Init(_side);

        _animation.SkeletonAnimation.AnimationState.Event += DestroyUnit;

        _stateMashine = new StateMashine();

        _stateMashine.AddState(new UnitMoveState(this, _stateMashine, _stopDistanceRadius.radius, _attackRadius.radius, _fov));
        _stateMashine.AddState(new UnitAttackState(this, _stateMashine, _attackRadius.radius));
        _stateMashine.AddState(new UnitDeathState(_animation));

        _stateMashine.SetState<UnitMoveState>();
    }

    private void OnDestroy()
    {
        _animation.SkeletonAnimation.AnimationState.Event -= DestroyUnit;

        Destroyed?.Invoke();
    }

    private void Update()
    {
        _stateMashine.Update();
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException();

        damage = damage > _currentHp ? _currentHp : damage;

        _currentHp -= damage;

        _view.UpdateHp(_data.Hp, _currentHp);

        if (_currentHp <= 0)
            _stateMashine.SetState<UnitDeathState>();
    }

    private void DestroyUnit(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name != DEATH_EVENT_NAME)
            return;

        Destroy(gameObject);
    }

    public SideEnum GetSide()
    {
        return _side;
    }

    public UnitTypeEnum GetUnitType()
    {
        return _data.UnitType;
    }
}
