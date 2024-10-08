using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTxt : MonoBehaviour
{
    Text txt;

    [SerializeField]
    float yposMove = 20f;
    [SerializeField]
    float scale = 1.1f;
    [SerializeField]
    float time = 0.5f;
    [SerializeField]
    float startYPos = 10f;

    Vector3 scaleVector;
    Sequence sequence = null;

    private void Awake()
    {
        scaleVector = new Vector3(scale, scale, 1f);
        txt = GetComponent<Text>();
    }

    public void SetDamage(string damageTxt)
    {
        txt.transform.localScale = new Vector3(1f, 1f, 1f);
        txt.text = damageTxt;

        transform.localPosition = new Vector3(transform.localPosition.x, startYPos, transform.localPosition.z);

        if (sequence != null)
        {
            sequence.Kill();
            sequence = null;
        }

        sequence = DOTween.Sequence().Append(txt.transform.DOLocalMoveY(startYPos + yposMove, time)).
            Join(txt.rectTransform.DOScale(scaleVector, time)).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
    }
}
