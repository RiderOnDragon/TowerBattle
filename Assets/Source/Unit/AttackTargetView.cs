using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackTargetView : MonoBehaviour
{
    [SerializeField] private Slider _hpBar;

    public void UpdateHp(float maxCount, float currentCount)
    {
        _hpBar.value = currentCount / maxCount;
    }
}
