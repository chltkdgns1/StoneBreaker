using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
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

    public void SetDamage(DamageData data)
    {
        remainHp -= data.damage;
        hpTxt.text = Utility.GetCommaNumberString(remainHp);
        hpBarImg.fillAmount = (float)remainHp / maxHp;
    }
}
