using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationInit : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GlobalData.Init();
    }
}
