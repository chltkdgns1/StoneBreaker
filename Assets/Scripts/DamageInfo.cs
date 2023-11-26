using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageState
{
    SUCCESS,
    MISS,
    DIE
}

public struct DamageData
{
    public DamageState state;
    public int damage;
    public DamageData(DamageState state, int damage)
    {
        this.state = state;
        this.damage = damage;
    }
}

[Serializable]
public struct ShieldInfo
{
    public int shield;
    public int miss;

    public DamageData GetDamage(AttackInfo attackInfo)
    {
        DamageData damageState = new DamageData(DamageState.SUCCESS, 0);

        attackInfo.damage -= shield;
        attackInfo.accuracy -= miss;

        if (attackInfo.damage <= 0)
            attackInfo.damage = 1;
        if (attackInfo.accuracy <= 0)
            attackInfo.accuracy = 1;

        if (attackInfo.accuracy < 100)
        {
            int range = UnityEngine.Random.Range(0, 100) + 1;
            if (attackInfo.accuracy < range)
            {
                damageState.damage = 0;
                damageState.state = DamageState.MISS;
                return damageState;
            }
        }

        damageState.damage = attackInfo.damage;
        return damageState;
    }
}

[Serializable]
public struct AttackInfo
{
    public int damage;
    public int accuracy;
}