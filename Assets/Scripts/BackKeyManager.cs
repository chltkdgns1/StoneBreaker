using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackKeyManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (BackKey.backyList.Count == 0)
                return;

            BackKey.backyList[BackKey.backyList.Count - 1].OnBack();
        }
    }
}