using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GlobalData
{
    public static EncryData money = null;
    public static EncryData stoneLevel = null;

    public static void Init()
    {
        Encry.Instance.Starting();
        money = new EncryData(0.ToString());
        stoneLevel = new EncryData(1.ToString());
    }
}
