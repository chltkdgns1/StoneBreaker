using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInGameScene : MonoBehaviour
{
    public interface EventHandler
    {
        void OnAddPick(int cnt);
    }

    EventHandler handler = null;

    public void SetHandler(EventHandler handler)
    {
        this.handler = handler;
    }

    public void OnAddPick(int cnt)
    {
        handler.OnAddPick(cnt);
    }
}
