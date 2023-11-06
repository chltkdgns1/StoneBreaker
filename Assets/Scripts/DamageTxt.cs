using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTxt : MonoBehaviour
{
    Text txt;
    public void SetDamage(string damageTxt)
    {
        txt.text = damageTxt;
        // 트윈 처리
    }
}
