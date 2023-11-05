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

    // 바위 반지름 만큼 diff 처리를 해야함. 그 상태에서 곡갱이질
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

    // 무빙이 끝나면 곡갱이질 시작
    public void OnMoveFinish()
    {
        SetPicking();
    }

    public PickAction GetPickAction()
    {
        return pickAction;
    }
}
