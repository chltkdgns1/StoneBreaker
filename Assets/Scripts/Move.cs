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

    protected virtual void FixedUpdate()
    {
        if (isPosMove == false && isDirMove == false)
            return;

        if (isPosMove)
        {
            Vector3 dir = (targetMove - transform.position);
            var nextMovePos = dir.normalized * speed * Time.deltaTime;
            SetMove(nextMovePos);
            //charController.Move(nextMovePos);

            if (IsFinishPosMove())
            {
                ResetMoveState();
                targetMove = Vector3.zero;
                distance = 1e9f;
                eventHandler?.OnMoveFinish();
            }
        }

        if (isDirMove)
        {
            var nextMovePos = dirMove * Time.deltaTime * speed;
            SetMove(nextMovePos);
            //charController.Move(nextMovePos);

            if (IsFinishDirMove())
            {
                ResetMoveState();
                dirMove = Vector3.zero;
                startPos = Vector3.zero;
                limitDistance = 10f;
                eventHandler?.OnMoveFinish();
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

        float dis = Vector3.Distance(targetMove, transform.position);
        if (distance > dis && dis > 0.001f)
        {
            distance = dis;
            return false;
        }
        return true;
    }

    protected virtual bool IsFinishDirMove()
    {
        if (isDirMove == false)
            return true;

        float distance = Vector3.Distance(startPos, transform.position);
        return distance >= limitDistance;
    }

    public virtual void MovePos(Vector3 pos)
    {
        isPosMove = true;
        isDirMove = false;
        targetMove = pos + transform.position;
        distance = 1e9f;
    }

    public virtual void MoveDir(Vector3 dir, float limitDistance = 10f)
    {
        isPosMove = false;
        isDirMove = true;
        dirMove = dir;
        startPos = transform.position;
        this.limitDistance = limitDistance;
    }
}
