using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveState : State
{
    private readonly StateMashine _stateMashine;
    private readonly Unit _user;
    private readonly float _stopDistance;
    private readonly float _attackDistance;
    private readonly float _fov;

    private Transform _currentTargetPoint;

    private int pointIndex;

    private bool _canMove;

    public UnitMoveState(Unit user, StateMashine stateMashine, float stopDistance, float attackDistance, float fov)
    {
        _stateMashine = stateMashine;
        _user = user;
        _stopDistance = stopDistance;
        _attackDistance = attackDistance;
        _fov = fov;

        if (_user.UnitSide == SideEnum.PLAYER)
        {
            pointIndex = 0;
        }
        else
        {
            pointIndex = Path.Singleton.PathPoints.Count - 1;
        }
    }

    public override void Enter()
    {
        if (_currentTargetPoint == null) 
            _currentTargetPoint = Path.Singleton.PathPoints[pointIndex];

        if (_canMove == false)
            _canMove = true;

        _user.Animation.StartRun();
    }

    public override void Update()
    {
        FieldOfViewCheck();

        if (_canMove == false)
            return;

        if ((Vector2)_user.transform.position != (Vector2)_currentTargetPoint.position)
        {
            var oldPosition = _user.transform.position;
            var newPosition = Vector2.MoveTowards(oldPosition, (Vector2)_currentTargetPoint.position, _user.Data.MoveSpeed * Time.deltaTime);
            _user.transform.position = newPosition;
        }
        else
        {
            if (_user.UnitSide == SideEnum.PLAYER)
                pointIndex++;
            else
                pointIndex--;

            if (pointIndex < 0 || pointIndex >= Path.Singleton.PathPoints.Count)
                return;

            _currentTargetPoint = Path.Singleton.PathPoints[pointIndex];
        }
    }

    private void FieldOfViewCheck()
    {
        var attackDistanceChecks = Physics2D.OverlapCircleAll(_user.transform.position, _attackDistance);
        var stopDistanceChecks = Physics2D.OverlapCircleAll(_user.transform.position, _stopDistance);

        if (attackDistanceChecks.Length > 0)
        {
            foreach (var item in attackDistanceChecks)
            {
                if (item.TryGetComponent(out IAttackTarget target) == false)
                    continue;

                if (target.GetSide() != _user.UnitSide)
                {
                    _canMove = false;
                    _stateMashine.SetState<UnitAttackState>();
                    return;
                }
            }
        }

        if (stopDistanceChecks.Length > 0)
        {
            foreach (var item in stopDistanceChecks)
            {
                if (item.TryGetComponent(out Unit unit) == false)
                    continue;

                if (unit == _user)
                    continue;

                Vector2 targetDirection = (unit.transform.position - _user.transform.position).normalized;

                if (Vector2.Angle(-_user.transform.right, targetDirection) > _fov / 2)
                {
                    if (unit.UnitSide == _user.UnitSide)
                    {
                        if (_canMove == true)
                        {
                            _canMove = false;
                            _user.Animation.StartIdle();
                        }

                        return;
                    }
                }
            }
        }

        if (_canMove == false)
        {
            _canMove = true;
            _user.Animation.StartRun();
        }
    }
}
