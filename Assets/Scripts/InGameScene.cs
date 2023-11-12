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

    [SerializeField]
    UIRelationInGameScene uIRelationInGameScene;

    public static InGameScene instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        uIInGameScene.SetHandler(this);
        uIRelationInGameScene.SetDamageTargetTxt(stoneController.transform);
    }

    public void AddPick(int cnt)
    {
        for(int i = 0; i < cnt; i++)
        {
            pickGroup.ActivePick(stoneController.transform, stoneController.GetRad());
        }
    }

    public void OnFinishPick(int damage, int accuracy)
    {
        var damageData = stoneController.SetDamage(damage, accuracy);
        uIRelationInGameScene.OnDamage(damageData);
    }

    public void OnAddPick(int cnt)
    {
        for(int i = 0; i < cnt; i++)
        {
            pickGroup.ActivePick(stoneController.transform, stoneController.GetRad());
        }
    }
}
