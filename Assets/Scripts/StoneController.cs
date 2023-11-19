using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageState
{
    SUCCESS,
    MISS,
    DIE,
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

public class StoneController : MonoBehaviour
{
    [SerializeField]
    float rad;

    int stoneHp = 30000000;
    int shield = 0;
    int miss = 0;

    DamageData data = new DamageData(DamageState.SUCCESS, 0);

    public float GetRad()
    {
        return rad;
    }

    public DamageData GetDamage(int damage, int accuracy)
    {
        damage -= shield;
        accuracy -= miss;

        if (damage <= 0)
            damage = 1;
        if (accuracy <= 0)
            accuracy = 1;

        if (accuracy < 100)
        {
            int range = Random.Range(0, 100) + 1;
            if (accuracy < range)
            {
                data.damage = 0;
                data.state = DamageState.MISS;
                return data;
            }
        }

        data.damage = damage;
        if (stoneHp <= 0)
        {
            data.state = DamageState.DIE;
            return data;
        }

        data.state = DamageState.SUCCESS;
        return data;
    }
}
