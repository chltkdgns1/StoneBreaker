using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScene : MonoBehaviour,
    PickController.EventHandler,
    UIInGameScene.EventHandler
{
    [SerializeField]
    PickGroupManager pickGroup;

    [SerializeField]
    Transform stoneGroup;

    StoneController stoneController;

    [SerializeField]
    UIInGameScene uIInGameScene;

    private void Awake()
    {
        uIInGameScene.SetHandler(this);
    }

    void Start()
    {
        CreateStone();
    }

    public void OnFinishPick(AttackInfo attackInfo)
    {
        stoneController.SetDamage(attackInfo);
        uIInGameScene.SetMoneyTxt();
    }

    public void OnAddPick(int cnt)
    {
        for(int i = 0; i < cnt; i++)
        {
            pickGroup.ActivePick(stoneController.transform, stoneController.GetRad(), this);
        }
    }

    void CreateStone()
    {
        int level = GlobalData.stoneLevel.GetData<int>();
        if (stoneController != null)
        {
            stoneController.SetDead();
            stoneController = null;
        }

        var stone = Resources.Load<GameObject>("DirectLink/Prefabs/Stone_" + level.ToString());
        stoneController = Instantiate(stone, stoneGroup).GetComponent<StoneController>();
        stoneController.transform.position = new Vector3(0, 1f, 0);
    }
}
