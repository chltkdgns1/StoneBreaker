using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageState
{
    SUCCESS,
    MISS,
    DIE,
}

public class StoneController : MonoBehaviour
{
    [SerializeField]
    float rad;

    int stoneHp;
    int shield;
    int miss;

    public float GetRad()
    {
        return rad;
    }

    public DamageState SetDamage(int damage, int accuracy)
    {
        damage -= shield;
        accuracy -= miss;

        if (damage <= 0)
            damage = 1;
        if (accuracy <= 0)
            accuracy = 1;

        if(accuracy < 100)
        {
            int range = Random.Range(0, 100) + 1;
            if(accuracy < range)
                return DamageState.MISS;
        }
        
        stoneHp -= damage;
        if (stoneHp <= 0)
            return DamageState.DIE;

        return DamageState.SUCCESS;
    }
}
