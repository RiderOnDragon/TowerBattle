using Spine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitAttackState : State
{
    private readonly Unit _user;
    private readonly StateMashine _stateMashine;

    private readonly float _attackDistance;

    private IAttackTarget _target;

    private Timer _currentTimer;

    private const string ATTACK_EVENT_NAME = "shot";

    public UnitAttackState(Unit user, StateMashine stateMashine, float attackDistance) 
    {
        _user = user;
        _stateMashine = stateMashine;
        _attackDistance = attackDistance;

        _user.Animation.SkeletonAnimation.AnimationState.Event += DealDamage;
    }

    ~UnitAttackState()
    {
        _user.Animation.SkeletonAnimation.AnimationState.Event -= DealDamage;

        if (_target != null)
            _target.Destroyed -= OnTargetDestroyed;

        if ( _currentTimer != null )
            _currentTimer.Finished -= Attack;
    }

    public override void Enter()
    {
        FindTarget();
    }

    public override void Update()
    {
        _currentTimer?.UpdateTime();
    }

    private void FindTarget()
    {
        if (_user == null)
            return;

        var targets = Physics2D.OverlapCircleAll(_user.transform.position, _attackDistance);

        foreach (var target in targets)
        {
            if (target.TryGetComponent(out IAttackTarget attackTarget) == false)
                continue;

            if (attackTarget.GetSide() != _user.UnitSide)
            {                
                _target = attackTarget;
                _target.Destroyed += OnTargetDestroyed;

                _user.Animation.StartIdle();

                _currentTimer = new Timer(_user.Data.AttackCooldown);
                _currentTimer.Finished += Attack;

                return;
            }
        }


        if (_target == null)
        {
            _stateMashine.SetState<UnitMoveState>();
            return;
        }
    }

    private void OnTargetDestroyed()
    {
        _target.Destroyed -= OnTargetDestroyed;
        _target = null;

        FindTarget();
    }

    private void Attack()
    {
        _currentTimer.Finished -= Attack;
        _currentTimer = null;

        _user.Animation.StartAttack();
    }

    private void DealDamage(TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name != ATTACK_EVENT_NAME)
            return;

        float damage = 0;

        if (_target.GetUnitType() == UnitTypeEnum.TOWER)
            damage = _user.Data.TowerDamage;
        else
            damage = _user.Data.UnitDamage;

        if (_target == null)
            return;

        if (_target.GetUnitType() == _user.Data.Superiority)
            damage *= UnitData.SUPERIORITY_DAMAGE_MULTIPLIER;
        else if (_target.GetUnitType() == _user.Data.Weakness)
            damage *= UnitData.WEAKNESS_DAMAGE_MULTIPLIER;

        _target.TakeDamage(damage);

        _currentTimer = new Timer(_user.Data.AttackCooldown);
        _currentTimer.Finished += Attack;
    }
}
