using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GlobalData
{
    public static EncryData money = null;
    public static void Init()
    {
        Encry.Instance.Starting();
        money = new EncryData(100000000.ToString());
    }
}
