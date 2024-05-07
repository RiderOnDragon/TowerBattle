using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiseUnitBottonView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _unitNameText;
    public void Init(UnitData unitData)
    {
        _unitNameText.text = $"{unitData.UnitType}";
    }
}
