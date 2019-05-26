using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class PartyController : MonoBehaviour
{
    Vector3 walkPos;
    float walkLocalScaleX;
    public void MoveButllePos(Vector3 pos)
    {
        walkPos = transform.position;
        walkLocalScaleX = transform.localScale.x;
        transform
             .DOMove(pos, 0.5f)
             .SetEase(Ease.InOutSine)
             .OnComplete(() =>
             {
                 SetLocalScaleX(-1);
             });
    }

    public void MoveBeforeBattlePos()
    {
        transform
             .DOMove(walkPos, 0.5f)
             .SetEase(Ease.InOutSine)
             .OnComplete(() =>
             {
                 SetLocalScaleX(walkLocalScaleX);
             })
             .OnComplete(() =>
             {
                 Params.gameMode = GameMode.MEMBER_MOVE_WALK_POS_COMPLETED;
             });
    }

    protected void SetLocalScaleX(float x)
    {
        float scaleX = x == 0 ? transform.localScale.x : -Mathf.Sign(x);
        float scaleY = transform.localScale.y;
        float scaleZ = transform.localScale.z;
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }

    public void SetRotate(float rotationX)
    {
        //cameraController.transform.localEulerAngles.x
        transform.rotation = Quaternion.Euler(rotationX, 0, 0);

    }

    public abstract void Walk();
}
