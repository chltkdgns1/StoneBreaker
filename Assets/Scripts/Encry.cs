using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public class EncryData
{
    // static ¸É¹ö
    static List<EncryData> encryList = new List<EncryData>();
    static private int keyValue = UnityEngine.Random.Range(13, 1 << 16);

    static StringBuilder stringBuilder = new StringBuilder();
    static List<string> decryList = new List<string>();

    // ·ÎÄÃ ¸É¹ö
    public string data;

    public EncryData(string data)
    {
        this.data = SetEncry(data);
        encryList.Add(this);
    }

    public void SetData(string data)
    {
        this.data = SetEncry(data);
    }

    private string SetEncry(string inputData)
    {
        stringBuilder.Clear();
        for (int i = 0; i < inputData.Length; i++)
        {
            stringBuilder.Append((char)(inputData[i] ^ (char)keyValue));
        }
        return stringBuilder.ToString();
    }

    string GetDecry()
    {
        return SetEncry(this.data);
    }

    public T GetData<T>()
    {
        return (T)Convert.ChangeType(GetDecry(), typeof(T));
    }
    
    public static void Reset() 
    {
        decryList.Clear();

        foreach (var encryEle in encryList)
        {
            decryList.Add(encryEle.GetDecry());
        }

        keyValue = UnityEngine.Random.Range(13, 1 << 16);

        for(int i = 0; i < encryList.Count; i++)
        {
            encryList[i].SetData(decryList[i]);
        }
    }

    public static void AllPrint()
    {
        foreach (var encryEle in encryList)
        {
            Debug.Log(encryEle.GetData<int>() + " , " + keyValue);
        }
    }
}

public class Encry : MonoSingleTon<Encry>
{
    float reInitTime = 1f;
    float sumTime = 0f;

    protected override void Init()
    {
        base.Init();
    }

    void ResetAll()
    {
        EncryData.Reset();
        EncryData.AllPrint();
    }

    private void Update()
    {
        sumTime += Time.deltaTime;
        if(sumTime >= reInitTime)
        {
            sumTime = 0f;
            ResetAll();
        }
    }

    public void Starting()
    {
       
    }
}
