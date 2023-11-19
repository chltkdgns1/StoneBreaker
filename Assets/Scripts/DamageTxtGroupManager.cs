using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTxtGroupManager : PrefabGroupManager
{
    public static DamageTxtGroupManager instance = null;

    protected override void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        base.Awake();
    }

    public void OnDamageTxt(DamageData data, Transform target)
    {
        var ob = GetNext();
        ob.transform.position = Camera.main.WorldToScreenPoint(target.position);
        ob.SetActive(true);

        var damageTxt = ob.GetComponent<DamageTxt>();

        if (data.state != DamageState.SUCCESS)
            damageTxt.SetDamage(data.state.ToString());
        else
            damageTxt.SetDamage(Utility.GetCommaNumberString(data.damage));
    }
}
