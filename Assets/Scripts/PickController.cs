using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickInfo
{
    public AttackInfo attackInfo;
}

public class PickController : MonoBehaviour, 
    Move.EventHandler, 
    PickAction.EventHandler
{
    Move move;

    [SerializeField]
    PickAction pickAction;

    [SerializeField]
    protected PickInfo pickInfo;

    float normalRad = 25f;

    public interface EventHandler
    {
        void OnFinishPick(AttackInfo attackInfo);
    }

    EventHandler eventHandler;

    private void Awake()
    {
        move = GetComponent<Move>();
        move.SetEventHandler(this);

        pickAction.SetHandler(this);

        SetRandomPosition();
    }

    void SetRandomPosition()
    {
        var xpos = Random.Range(0f, 5f) * GetRandSign();
        var zpos = Mathf.Sqrt(normalRad - xpos * xpos) * GetRandSign();
        transform.position = new Vector3(xpos, 0.5f, zpos);
    }

    int GetRandSign()
    {
        return Random.Range(0, 2) == 0 ? 1 : -1;
    }

    // 바위 반지름 만큼 diff 처리를 해야함. 그 상태에서 곡갱이질
    public void SetMove(Transform target, float diff = 0f)
    {
        move.MovePos(target, diff);
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

    public void OnFinishPick()
    {
        eventHandler.OnFinishPick(pickInfo.attackInfo);
    }

    public void SetHandler(PickController.EventHandler handler)
    {
        eventHandler = handler;
    }
}
