using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public interface EventHandler
    {
        void OnMoveFinish();
    }

    EventHandler eventHandler = null;

    protected CharacterController charController;

    [SerializeField]
    protected float speed = 5f;

    protected float distance = 1e9f;

    protected bool isPosMove = false;
    protected bool isDirMove = false;
    protected Vector3 targetMove = Vector3.zero;
    protected Vector3 dirMove = Vector3.zero;
    protected Vector3 startPos = Vector3.zero;
    protected float limitDistance = 10f;

    protected virtual void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    public void SetEventHandler(EventHandler handler)
    {
        this.eventHandler = handler;
    }

    protected virtual void ResetMoveState()
    {
        isDirMove = false;
        isPosMove = false;
    }

    public virtual void Stop()
    {
        ResetMoveState();
    }

    public Vector3 GetXZ(Vector3 pos)
    {
        return new Vector3(pos.x, 0, pos.z);
    }

    protected virtual void FixedUpdate()
    {
        if (isPosMove == false && isDirMove == false)
            return;

        if (isPosMove)
        {
            Vector3 dir = (targetMove - GetXZ(transform.position));
            var nextMovePos = dir.normalized * speed * Time.deltaTime;
            SetMove(nextMovePos);

            if (IsFinishPosMove())
            {
                ResetMoveState();
                targetMove = Vector3.zero;
                distance = 1e9f;
                eventHandler?.OnMoveFinish();
                return;
            }
        }
    }

    public virtual void SetMove(Vector3 pos)
    {
        if (charController != null)
            charController.Move(pos);
        else
            transform.position += pos;
    }

    protected virtual bool IsFinishPosMove()
    {
        if (isPosMove == false)
            return true;

        float dis = Vector3.Distance(targetMove, GetXZ(transform.position));
        if (distance > dis && dis > 0.001f)
        {
            distance = dis;
            return false;
        }
        return true;
    }

    public virtual void MovePos(Transform pos, float diff = 0f)
    {
        isPosMove = true;
        isDirMove = false;

        transform.LookAt(pos);
        var dir = (GetXZ(transform.position) - GetXZ(pos.position)).normalized;
        targetMove = dir * diff + GetXZ(pos.position);
        distance = 1e9f;
    }
}
