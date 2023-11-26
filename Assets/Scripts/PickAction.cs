using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickAction : MonoBehaviour
{
    Sequence mySequence = null;

    Vector3 down = new Vector3(0, 0f, 60f);
    Vector3 up = new Vector3(0f, 0f, 0f);

    float upTime = 0.5f;
    float downTime = 0.3f;

    public interface EventHandler
    {
        void OnFinishPick();
    }

    EventHandler handler = null;

    public void SetHandler(EventHandler handler)
    {
        if (this.handler != null)
            return;

        this.handler = handler;
    }

    public void SetPicking()
    {
        if(mySequence != null)
        {
            mySequence.Kill();
            mySequence = null;
        }

        mySequence = DOTween.Sequence();
        mySequence.Append(transform.DOLocalRotate(down, downTime)).AppendCallback(() =>
        {
            handler.OnFinishPick();
            // 데미지 입힌다.
        }).Append(transform.DOLocalRotate(up, upTime)).SetLoops(-1);
    }
}
