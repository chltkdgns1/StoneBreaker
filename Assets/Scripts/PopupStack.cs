using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class PopupStack : BackKey
{
    public Action onClose = null;
    public Action backKeyEvent = null;

    #region 팝업 스택 static
    static Dictionary<GameObject, string> _dic = new Dictionary<GameObject, string>();
    static Dictionary<string, GameObject> _dicStr = new Dictionary<string, GameObject>();
    static List<GameObject> popupList = new List<GameObject>();
    static GameObject canvasObject = null;
    static Dictionary<string, GameObject> _dicPrefabs = new Dictionary<string, GameObject>();
    #endregion

    protected override void OnEnable()
    {
        gameObject.name = gameObject.name.Replace("(Clone)", "");
        base.OnEnable();
        AddPopup();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        RemovePopup();
    }

    public override void OnBack()
    {
        base.OnBack();
        gameObject.SetActive(false);
    }

    public virtual void AddPopup()
    {
        AddPopup(gameObject);
    }
    public virtual void RemovePopup()
    {
        if (gameObject == null) 
            return;
   
        RemovePopup(gameObject);
    }

    public void SetDirty()
    {
        if (this == null) return;
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
#endif
    }

    #region 팝업 스택 static
    static public T PopupShow<T>(string popupPath)
    {
        if (canvasObject == null)
        {
            canvasObject = GameObject.Find("Canvas");
        }

        var splitList = popupPath.Split("/");

        string name = splitList[splitList.Length - 1];
        Transform popup = canvasObject.transform.Find(name);

        if (popup == null)
        {
            GameObject popupPrefabs = null;
            if (_dicPrefabs.ContainsKey(popupPath) == true)
                popupPrefabs = _dicPrefabs[popupPath];
            else
            {
                popupPrefabs = Resources.Load<GameObject>(popupPath);
                _dicPrefabs.Add(popupPath, popupPrefabs);
            }

            popup = Instantiate(popupPrefabs, canvasObject.transform).transform;
        }
        popup.gameObject.SetActive(true);
        return popup.GetComponent<T>();
    }

    static public T CheckShowPopup<T>(string popupPath) where T : MonoBehaviour
    {
        if (string.IsNullOrEmpty(popupPath))
            return null;

        var splitList = popupPath.Split("/");
        var name = splitList[splitList.Length - 1];

        if (_dicStr.ContainsKey(name))
            return _dicStr[name].GetComponent<T>();

        return null;
    }

    static void AddPopup(GameObject ob)
    {
        ob.transform.SetAsLastSibling();

        if (popupList.Contains(ob) == true)
            popupList.Remove(ob);

        popupList.Add(ob);
        AddDicData(ob);
    }
    // 코드에 이벤트에 의해서 작동하는 경우
    static void RemovePopup(GameObject ob)
    {
        Remove(ob);
    }
    // 뒤로 가기에 의해서 작동하는 경우
    static public bool RemoveBack()
    {
        if (IsEmpty()) 
            return false;

        GameObject popup = GetLastPopup();

        if (popup == null)
        {
            RefreshPopup();
            return RemoveBack();
        }

        RemoveDicData(popup);
        popup.GetComponent<PopupStack>()?.onClose?.Invoke();
        return true;
    }

    static GameObject GetLastPopup()
    {
        return popupList[popupList.Count - 1];
    }

    static void RefreshPopup()
    {
        var last = popupList[popupList.Count - 1];
        if (last == null)
            popupList.RemoveAt(popupList.Count - 1);
    }

    static void AddDicData(GameObject ob)
    {
        if (_dic.ContainsKey(ob))
            _dic[ob] = ob.name;
        else
            _dic.Add(ob, ob.name);

        if (_dicStr.ContainsKey(ob.name))
            _dicStr[ob.name] = ob;
        else
            _dicStr.Add(ob.name, ob);
    }

    static void RemoveDicData(GameObject ob)
    {
        _dic.Remove(ob);
        _dicStr.Remove(ob.name);
    }

    static public void Remove(string popupName)
    {
        if (IsEmpty())
        {
            Debug.Log("popup empty");
            return;
        }

        if (_dicStr.ContainsKey(popupName) == false)
        {
            Debug.Log("popupName dont exists");
            return;
        }

        var ob = _dicStr[popupName];
        var popup = ob.GetComponent<PopupStack>();
        popup.onClose?.Invoke();
        RemoveDicData(ob);
    }
    static void Remove(GameObject ob)
    {
        if (IsEmpty())
        {
            Debug.Log("popup empty");
            return;
        }
        if (_dic.ContainsKey(ob) == false)
        {
            Debug.Log("popup dont exists");
            return;
        }

        var popup = ob.GetComponent<PopupStack>();
        popup.onClose?.Invoke();
        RemoveDicData(ob);
    }
    static public bool IsEmpty()
    {
        return popupList.Count == 0;
    }
    #endregion
}
