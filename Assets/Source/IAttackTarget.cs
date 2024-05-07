using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackTarget
{
    void TakeDamage(float damage);
    SideEnum GetSide();
    UnitTypeEnum GetUnitType();
    event Action Destroyed;
}
