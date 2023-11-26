using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class PopupStack : MonoBehaviour
{
    public Action onClose = null;

    #region 팝업 스택 static
    static Dictionary<GameObject, bool> _dic = new Dictionary<GameObject, bool>();
    static Dictionary<string, int> _dicStr = new Dictionary<string, int>();
    static Stack<GameObject> popupStack = new Stack<GameObject>();
    static GameObject canvasObject = null;
    #endregion

    protected virtual void OnEnable()
    {
        AddPopup();
    }

    protected virtual void OnDisable()
    {
        RemovePopup();
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

        Transform popup = canvasObject.transform.Find(splitList[splitList.Length - 1]);

        if (popup == null)
        {
            GameObject popupPrefabs = Resources.Load<GameObject>(popupPath);
            popup = Instantiate(popupPrefabs, canvasObject.transform).transform;
            popup.SetAsLastSibling();
        }
        return popup.GetComponent<T>();
    }

    static void AddPopup(GameObject ob)
    {
        Debug.LogWarning("에드 네임 : " + ob.name);
        ob.transform.SetAsLastSibling();
        popupStack.Push(ob);
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
        if (popupStack.Count == 0) 
            return false;

        GameObject ob = popupStack.Pop();

        if (ob == null)
        {
            RefreshStack();
            return RemoveBack();
        }

        RemoveDicData(ob);
        ob.GetComponent<PopupStack>()?.onClose?.Invoke();
        return true;
    }

    static void RefreshStack()
    {
        List<GameObject> popList = new List<GameObject>();

        while(popupStack.Count != 0)
        {
            var ob = popupStack.Pop();
            if (ob != null)
                popList.Add(ob);
        }

        for(int i = popList.Count -1; i >=0; i--)
        {
            popupStack.Push(popList[i]);
        }
    }

    static void AddDicData(GameObject ob)
    {
        _dic.Add(ob, true);

        if (_dicStr.ContainsKey(ob.name))
        {
            _dicStr[ob.name]++;
        }
        else
        {
            _dicStr[ob.name] = 1;
        }
    }
    static void RemoveDicData(GameObject ob)
    {
        _dic.Remove(ob);

        if (_dicStr.ContainsKey(ob.name))
        {
            _dicStr[ob.name]--;
            if (_dicStr[ob.name] == 0)
            {
                _dicStr.Remove(ob.name);
            }

            else if (_dicStr[ob.name] < 0)
            {
                Debug.LogError("static void RemoveDicData(GameObject ob) < 0");
                _dicStr.Remove(ob.name);
            }
        }
    }
    static public void Remove(string popupName)
    {
        if (popupStack.Count == 0)
        {
            Debug.Log("popup empty");
            return;
        }

        if (_dicStr.ContainsKey(popupName) == false)
        {
            Debug.Log("popupName dont exists");
            return;
        }

        while (popupStack.Count != 0)
        {
            GameObject temp = popupStack.Pop();
            PopupStack tempStack = temp.GetComponent<PopupStack>();
            tempStack.onClose?.Invoke();

            RemoveDicData(temp);
            if (_dicStr.ContainsKey(temp.name) == false)
            {
                break;
            }
        }
    }
    static void Remove(GameObject ob)
    {
        if (popupStack.Count == 0)
        {
            Debug.Log("popup empty");
            return;
        }
        if (_dic.ContainsKey(ob) == false)
        {
            Debug.Log("popup dont exists");
            return;
        }

        while (popupStack.Count != 0)
        {
            GameObject temp = popupStack.Pop();
            PopupStack tempStack = temp.GetComponent<PopupStack>();
            tempStack?.onClose?.Invoke();

            RemoveDicData(temp);
            if (ob == temp) break;
        }
    }
    static public bool IsEmpty()
    {
        return popupStack.Count == 0;
    }
    #endregion
}
