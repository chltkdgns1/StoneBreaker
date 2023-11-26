using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackKey : MonoBehaviour
{
    static public List<BackKey> backyList = new List<BackKey>();

    protected virtual void OnEnable()
    {
        backyList.Add(this);
    }

    protected virtual void OnDisable()
    {
        backyList.Remove(this);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (backyList.Count == 0)
                return;

            backyList[backyList.Count - 1].OnBack();
        }
    }

    public virtual void OnBack()
    {

    }
}
