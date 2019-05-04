using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    bool isDoMoveZ;
    bool isDoMoveX;
    Sequence sequence;
    float walkSpeed;
    float doMoveTargetZ;
    float takeOffPointZ;
    Vector3 tapPos;
    float axis;
    float time;
    float axisTime;
    float keyX;
    float keyZ;
    [NonSerialized] public float dx;
    public void Init()
    {
        walkSpeed = Constants.DEFAULT_WALK_SPEED;
        rb = GetComponent<Rigidbody>();
    }



    void Update()
    {

        WalkRoad();


    }

    float GetAxisForTap()
    {

        if (axis == 1 || axis == -1)
        {
            return axis;
        }
        axisTime = time;
        time += Time.deltaTime * 2;
        axis = Mathf.Clamp(keyX * Mathf.Sqrt(time), -1, 1);
        return axis;
    }

    float GetAxisForRerease()
    {

        time -= Time.deltaTime * 4;
        if (time <= 0)
        {
            time = 0;
            return 0;
        }

        axis = keyX * Mathf.Sqrt(time);
        // Debug.Log(axis);
        return axis;
    }

    void WalkRoad()
    {


        if (Input.GetMouseButtonDown(0))
        {
            time = 0;
            keyX = 0;
            tapPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            SetTapKeys();
            dx = GetAxisForTap();
        }
        else
        {
            dx = GetAxisForRerease();
        }



        dx *= walkSpeed;
        RoadController road = GetRoad();
        RoadType roadType = RoadType.DEFAULT;
        if (road) roadType = road.roadType;
        switch (roadType)
        {
            case RoadType.HORIZONTAL:
                break;
            case RoadType.VERTICAL:
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

    void SetTapKeys()
    {
        Vector3 swipeVec = Input.mousePosition - tapPos;
        float theta = (float)(Mathf.Atan2(swipeVec.y, swipeVec.x) * 180 / Math.PI);

        float absTheta = Mathf.Abs(theta);
        if (absTheta > 180 - Constants.HORIZONTAL_DEG)
        {
            keyX = -1;
        }

        if (Constants.HORIZONTAL_DEG > absTheta && absTheta > 0)
        {
            keyX = 1;
        }

        keyZ = 0;
        if (180 - Constants.VERTICAL_DEG > theta && theta > Constants.VERTICAL_DEG)
        {
            keyZ = 1;
        }
        if (-Constants.VERTICAL_DEG > theta && theta > Constants.VERTICAL_DEG - 180)
        {
            keyZ = -1;
        }
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
        walkSpeed = Constants.VIRTICAL_WALK_SPEED;
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
            .DOMoveX(x, Constants.DO_MOVE_X_SEC)
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
        .DOMoveZ(doMoveTargetZ, Constants.DO_MOVE_Z_SEC)
        .SetEase(Ease.InOutSine)
        .OnComplete(() =>
        {
            time = 0;
            axis = 0;
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
