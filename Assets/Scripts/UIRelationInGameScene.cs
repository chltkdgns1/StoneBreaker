using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRelationInGameScene : MonoBehaviour
{
    [SerializeField]
    DamageTxtGroupManager damageTxtGroup;

    [SerializeField]
    HPBar stoneHpBar;

    public void OnDamage(DamageData data)
    {
        damageTxtGroup.OnDamageTxt(data);
        stoneHpBar.SetDamage(data);
    }

    public void SetDamageTargetTxt(Transform target)
    {
        damageTxtGroup.SetTarget(target);
    }
}
