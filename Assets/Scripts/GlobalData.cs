using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GlobalData
{
    static GlobalData instance = new GlobalData();

    GlobalData()
    {

    }

    public static void Init()
    {
        Encry.Instance.Starting();

        var encry = new EncryData(3.ToString());
        Debug.Log(encry.GetData<int>());
    }
}
