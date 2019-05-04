using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MemberController : MonoBehaviour
{

    NavMeshAgent agent;
    PlayerController playerController;
    [SerializeField] int partyNum;
    [SerializeField] Transform target;

    void Start()
    {
        playerController = SerializeManager.i.playerController;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {

        FollowTarget();
    }

    void FollowTarget()
    {
        Vector3 targetPos = target.position;
        Vector3 targetToThisVec = transform.position - targetPos;

        if (targetToThisVec.magnitude > Constants.CHARACTER_FOLLOW_OFFSET)
        {
            transform.position = targetPos + targetToThisVec.normalized * Constants.CHARACTER_FOLLOW_OFFSET;
        }

        if (targetToThisVec.x != 0)
        {
            float key = Mathf.Sign(targetToThisVec.x);
            LocalScale(key);
        }
    }

    void LocalScale(float key)
    {
        float scaleX = key == 0 ? transform.localScale.x : key;
        float scaleY = transform.localScale.y;
        float scaleZ = transform.localScale.z;
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }

}
