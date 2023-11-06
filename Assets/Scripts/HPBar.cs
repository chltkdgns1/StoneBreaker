using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    PrefabGroupManager damageTxtGroup;

    [SerializeField]
    Image hpBarImg;

    [SerializeField]
    Text hpTxt;

    int remainHp;
    int maxHp;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetHp(int hp)
    {
        remainHp = hp;
        maxHp = hp;
    }

    public void SetDamage(int damage, DamageState state)
    {
        remainHp -= damage;
        hpTxt.text = Utility.GetCommaNumberString(remainHp);
        hpBarImg.fillAmount = (float)remainHp / maxHp;

        var damageTxt = damageTxtGroup.GetNext();

        var damTxt = damageTxt.GetComponent<DamageTxt>();

        if (state == DamageState.DIE)
            damTxt.SetDamage("DIE");
        else if (state == DamageState.MISS)
            damTxt.SetDamage("MISS");
        else
            damTxt.SetDamage(Utility.GetCommaNumberString(damage));
    }
}
