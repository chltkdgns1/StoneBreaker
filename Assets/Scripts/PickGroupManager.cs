using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickGroupManager : PrefabGroupManager
{
    public void ActivePick(Transform target, float diff, PickAction.EventHandler handler)
    {
        var ob = GetNextCreate();
        if (ob == null)
        {
            Debug.LogError("[ActivePick] - ob is null");
            return;
        }

        ob.SetActive(true);
        var pickController = ob.GetComponent<PickController>();
        pickController.SetMove(target, diff);
        pickController.SetActionHander(handler);
    }

    public List<GameObject> GetPickList()
    {
        return objectPool;
    }
}
