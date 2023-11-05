using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickGroupManager : PrefabGroupManager
{
    public void ActivePick(Vector3 target, float diff)
    {
        var ob = GetNextCreate();
        if (ob == null)
        {
            Debug.LogError("[ActivePick] - ob is null");
            return;
        }

        var pickController = ob.GetComponent<PickController>();
        pickController.SetMove(target, diff);
    }

    public List<GameObject> GetPickList()
    {
        return objectPool;
    }
}
