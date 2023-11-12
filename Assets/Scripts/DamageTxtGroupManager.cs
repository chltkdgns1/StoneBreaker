using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTxtGroupManager : PrefabGroupManager
{
    private Transform damageTarget;

    public void OnDamageTxt(DamageData data)
    {
        var damageTxt = GetNext().GetComponent<DamageTxt>();

        damageTxt.transform.position = damageTarget.position;

        if (data.state != DamageState.SUCCESS)
            damageTxt.SetDamage(data.state.ToString());
        else
            damageTxt.SetDamage(Utility.GetCommaNumberString(data.damage));
    }

    public void SetTarget(Transform target)
    {
        damageTarget = target;
    }
}
