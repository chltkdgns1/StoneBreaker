using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickAction : MonoBehaviour
{
    Sequence mySequence = null;

    Vector3 down = new Vector3(-120f, 0f, 0f);
    Vector3 up = new Vector3(120f, 0f, 0f);

    float duringTime = 1f;

    int damage = 1;
    int accuracy = 100;

    public interface EventHandler
    {
        void OnFinishPick(int damage, int accuracy);
    }

    EventHandler handler = null;

    public void SetHandler(EventHandler handler)
    {
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
        mySequence.Append(transform.DORotate(down, duringTime)).OnComplete(() =>
        {
            handler.OnFinishPick(damage, accuracy);
            // 데미지 입힌다.
        }).Append(transform.DORotate(up, duringTime));
    }
}
