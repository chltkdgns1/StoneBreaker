using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickController : MonoBehaviour, Move.EventHandler
{
    Move move;
    PickAction pickAction;

    private void Awake()
    {
        move = GetComponent<Move>();
        pickAction = GetComponent<PickAction>();
    }

    // ���� ������ ��ŭ diff ó���� �ؾ���. �� ���¿��� �����
    public void SetMove(Vector3 target, float diff = 0f)
    {
        var dir = (target - transform.position).normalized;
        var realTarget = -dir * diff + target;
        move.MovePos(realTarget);
    }

    public void SetPicking()
    {
        pickAction.SetPicking();
    }

    // ������ ������ ����� ����
    public void OnMoveFinish()
    {
        SetPicking();
    }

    public PickAction GetPickAction()
    {
        return pickAction;
    }
}
