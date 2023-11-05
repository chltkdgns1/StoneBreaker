using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameScene : MonoBehaviour,
    PickAction.EventHandler
{
    [SerializeField]
    PickGroupManager pickGroup;

    [SerializeField]
    StoneController stoneController;

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

    public void AddPick(int cnt)
    {
        for(int i = 0; i < cnt; i++)
        {
            pickGroup.ActivePick(stoneController.transform.position, stoneController.GetRad());
        }
    }

    public void OnFinishPick(int damage, int accuracy)
    {

    }
}
