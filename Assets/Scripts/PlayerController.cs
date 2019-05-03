using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerController : MonoBehaviour
{


    bool isDoMoveZ;
    bool isDoMoveX;
    Sequence sequence;
    float walkSpeed;
    float doMoveTargetZ;
    float takeOffPointZ;
    Vector3 tapPos;


    public void Init()
    {
        walkSpeed = Constants.DEFAULT_WALK_SPEED;
    }



    void Update()
    {

        float dx = Input.GetAxis("Horizontal") * walkSpeed;
        float dz = Input.GetAxis("Vertical");
        float keyZ = dz != 0 ? Mathf.Sign(dz) : 0;
        //Debug.Log(dx);

        if (Input.GetMouseButtonDown(0))
        {
            tapPos = Input.mousePosition;
            //Debug.Log(tapPos);
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 swipeVec = Input.mousePosition - tapPos;
            //Debug.Log(swipeVec);
            float theta = (float)(Mathf.Atan2(swipeVec.y, swipeVec.x) * 180 / Math.PI);
            //Debug.Log(theta);
            if (Mathf.Abs(theta) > 100)
            {
                dx = -walkSpeed;
            }

            if (80 > Mathf.Abs(theta) && Mathf.Abs(theta) > 0)
            {
                dx = walkSpeed;
            }

            if (100 > theta && theta > 80)
            {
                keyZ = 1;
            }
            if (-80 > theta && theta > -100)
            {
                keyZ = -1;
            }
        }









        RoadController road = GetRoad();
        if (!road) return;
        switch (road.roadType)
        {
            case RoadType.HORIZONTAL:
                break;
            case RoadType.VERTICAL:
                /*  if (dz != 0)
                    {
                        sequence.Pause();
                        isDoMoveZ = false;
                        doMoveTargetZ = takeOffPointZ;
                        DoMoveZ(Mathf.Sign(dz));
                    }*/


                break;
            case RoadType.T:

                if (keyZ < 0) VerticalMove(keyZ, road);
                break;
            case RoadType.T_REVERSE:
                if (keyZ > 0) VerticalMove(keyZ, road);
                break;
            case RoadType.LEFT_STOP:
                dx = LimitedDx(road, true, dx);
                break;
            case RoadType.RIGHT_STOP:
                dx = LimitedDx(road, false, dx);
                break;
            default:
                break;
        }
        transform.Translate(dx, 0, 0);

        LocalScale(dx);
    }



    float LimitedDx(RoadController road, bool isLeft, float dx)
    {
        if (!road.stopProperty) return dx;
        float delta = road.stopProperty.stopPoint - transform.position.x;

        int key = isLeft ? 1 : -1;
        if (delta * key < 0) return dx;
        if (dx * key >= 0) return dx;
        return 0;
    }

    void LocalScale(float dx)
    {
        float scaleX = dx == 0 ? transform.localScale.x : -Mathf.Sign(dx);
        float scaleY = transform.localScale.y;
        float scaleZ = transform.localScale.z;
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }

    void VerticalMove(float key, RoadController road)
    {
        walkSpeed = 0.15f;
        DoMoveX(road.transform.position.x);
        takeOffPointZ = road.transform.position.z;
        doMoveTargetZ = takeOffPointZ + 20.0f * key;
        DoMoveZ(key);

    }

    void DoMoveX(float x)
    {
        if (isDoMoveX) return;
        isDoMoveX = true;
        transform
            .DOMoveX(x, 0.8f)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                isDoMoveX = false;
            });
    }

    void DoMoveZ(float key)
    {
        if (isDoMoveZ) return;

        sequence = DOTween.Sequence();
        sequence.Append(
            transform
        .DOMoveZ(doMoveTargetZ, 1.5f)
        .SetEase(Ease.InOutSine)
        .OnComplete(() =>
        {
            isDoMoveZ = false;
            walkSpeed = Constants.DEFAULT_WALK_SPEED;
            sequence.Kill();
        })
        );

        isDoMoveZ = true;
    }


    RoadController GetRoad()
    {
        Vector3 rayVec = transform.position + new Vector3(0, -10, 0);
        //Vector3 rayVec = new Vector3(0, -10, 0);

        //Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
        Ray ray = new Ray(transform.position, new Vector3(0, -10, 0));

        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit hit;

        //Rayの飛ばせる距離
        int distance = 10;

        //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        Debug.DrawLine(ray.origin, ray.direction * distance, Color.red);

        //もしRayにオブジェクトが衝突したら
        //                  ↓Ray  ↓Rayが当たったオブジェクト ↓距離
        if (Physics.Raycast(ray, out hit, distance))
        {
            // Debug.Log(roadType);
            return hit.collider.GetComponent<RoadController>();
        }

        return null;
    }
}
