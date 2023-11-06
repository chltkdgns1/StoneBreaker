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
    }

    void Start()
    {
        uIInGameScene.SetHandler(this);
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
        stoneController.SetDamage(damage, accuracy);
    }

    public void OnAddPick(int cnt)
    {
        for(int i = 0; i < cnt; i++)
        {
            pickGroup.ActivePick(stoneController.transform, stoneController.GetRad());
        }
    }
}
