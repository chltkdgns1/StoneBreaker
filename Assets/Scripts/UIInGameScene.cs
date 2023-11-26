using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGameScene : MonoBehaviour
{
    [SerializeField]
    public Text money;

    public interface EventHandler
    {
        void OnAddPick(int cnt);
    }

    EventHandler handler = null;

    private void Start()
    {
        SetMoneyTxt();
    }

    public void SetMoneyTxt()
    {
        money.text = Utility.GetCommaNumberString(GlobalData.money.GetData<long>());
    }

    public void SetHandler(EventHandler handler)
    {
        this.handler = handler;
    }

    public void OnAddPick(int cnt)
    {
        handler.OnAddPick(cnt);
    }

    public void OnForcePopup()
    {
        var popup = PopupStack.CheckShowPopup<PopupForcePick>("DirectLink/Prefabs/PopupPickForce");
        if(popup != null)
            popup.SetData();
        else
            PopupStack.PopupShow<PopupForcePick>("DirectLink/Prefabs/PopupPickForce");
    }
}
