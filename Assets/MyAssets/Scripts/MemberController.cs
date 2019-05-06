using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class MemberController : PartyController
{
    //[SerializeField] int partyNum;
    [SerializeField] Transform target;

    public override void Walk()
    {
        FollowTarget();
    }

    public void FollowTarget()
    {
        Vector3 targetPos = target.position;
        Vector3 targetToThisVec = transform.position - targetPos;

        if (targetToThisVec.magnitude > Constants.CHARACTER_FOLLOW_OFFSET)
        {
            transform.position = targetPos + targetToThisVec.normalized * Constants.CHARACTER_FOLLOW_OFFSET;
        }

        if (targetToThisVec.x != 0)
        {
            SetLocalScaleX(-targetToThisVec.x);
        }
    }

}
