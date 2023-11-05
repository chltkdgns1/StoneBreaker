using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabGroupManager : MonoBehaviour
{
    [SerializeField]
    protected GameObject prefabs;
    [SerializeField]
    protected Transform parentTrans;
    [SerializeField]
    int initPoolSize;
    [SerializeField]
    int addSize = 10;

    protected List<GameObject> objectPool = new List<GameObject>();
    protected int now = 0;

    int poolSize = 0;


    protected virtual void Awake()
    {
        if(prefabs == null)
        {
            Debug.LogError("prefabs is null");
            return; 
        }

        if(poolSize == 0)
        {
            Debug.LogError("poolSize is zeror");
            return;
        }

        now = 0;
        if (parentTrans == null)
            parentTrans = gameObject.transform;

        AddPoolSize(initPoolSize);
    }

    void AddPoolSize(int size)
    {
        poolSize += size;

        for(int i = 0;i < size; i++)
        {
            var ob = Instantiate(prefabs, parentTrans);
            objectPool.Add(ob);
            ob.SetActive(false);
        }
    }

    public virtual GameObject GetNext()
    {
        if (poolSize == 0)
        {
            Debug.LogError("[GetNext] - Next poolSize is zeror");
            return null;
        }

        now %= poolSize;
        return objectPool[now++];
    }

    public virtual GameObject GetNextCreate()
    {
        if (poolSize == 0)
        {
            Debug.LogError("[GetNextCreate] - Next poolSize is zeror");
            return null;
        }

        if(now == poolSize)
            AddPoolSize(addSize);

        return GetNext();
    }
}
