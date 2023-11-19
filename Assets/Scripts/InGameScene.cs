using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScene : MonoBehaviour,
    PickAction.EventHandler,
    UIInGameScene.EventHandler
{
    [SerializeField]
    PickGroupManager pickGroup;

    [SerializeField]
    StoneController stoneController;

    [SerializeField]
    UIInGameScene uIInGameScene;

    private void Awake()
    {
        uIInGameScene.SetHandler(this);
    }

    public void AddPick(int cnt)
    {
        for(int i = 0; i < cnt; i++)
        {
            pickGroup.ActivePick(stoneController.transform, stoneController.GetRad(), this);
        }
    }

    public void OnFinishPick(int damage, int accuracy)
    {
        var damageData = stoneController.GetDamage(damage, accuracy);
        DamageTxtGroupManager.instance.OnDamageTxt(damageData, stoneController.transform);
        long money = GlobalData.money.GetData<long>() + damageData.damage;
        GlobalData.money.SetData(money.ToString());
        uIInGameScene.SetMoneyTxt();
    }

    public void OnAddPick(int cnt)
    {
        for(int i = 0; i < cnt; i++)
        {
            pickGroup.ActivePick(stoneController.transform, stoneController.GetRad(), this);
        }
    }
}
