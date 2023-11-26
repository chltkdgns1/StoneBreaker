using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoneInfo
{
    public int hp;
    public ShieldInfo shield;

    public DamageData SetDamage(AttackInfo attackInfo)
    {
        var damageInfo = shield.GetDamage(attackInfo);
        if(hp <= damageInfo.damage)
        {
            hp = 0;
            damageInfo.state = DamageState.DIE;
        }
        else
        {
            hp -= damageInfo.damage;
        }
        return damageInfo;
    }
}

public class StoneController : MonoBehaviour
{
    [SerializeField]
    float rad;

    [SerializeField]
    protected StoneInfo stoneInfo;

    public float GetRad()
    {
        return rad;
    }

    public void SetDamage(AttackInfo attack)
    {
        var damageData = stoneInfo.SetDamage(attack);
        DamageTxtGroupManager.instance.OnDamageTxt(damageData, transform);
        long money = GlobalData.money.GetData<long>() + damageData.damage;
        GlobalData.money.SetData(money.ToString());
    }

    public void SetDead()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
