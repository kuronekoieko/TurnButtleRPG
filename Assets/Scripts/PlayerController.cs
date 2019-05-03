using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{


    bool isDoMoveZ;
    bool isDoMoveX;
    Sequence sequence;
    float walkSpeed;

    public void Init()
    {
        walkSpeed = Constants.DEFAULT_WALK_SPEED;
    }



    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * walkSpeed;
        float dz = Input.GetAxis("Vertical");
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;

        RoadController road = GetRoad();
        if (!road) return;
        switch (road.roadType)
        {
            case RoadType.HORIZONTAL:
                break;
            case RoadType.VERTICAL:
                break;
            case RoadType.T:
                if (dz < 0) VerticalMove(dz, road.transform.position.x);
                break;
            case RoadType.T_REVERSE:
                if (dz > 0) VerticalMove(dz, road.transform.position.x);
                break;
            case RoadType.STOP:

                break;
            default:
                break;
        }
        transform.position = new Vector3(x + dx, y, z);

        LocalScale(dx);
    }

    void LocalScale(float dx)
    {
        float scaleX = dx == 0 ? transform.localScale.x : -Mathf.Sign(dx);
        float scaleY = transform.localScale.y;
        float scaleZ = transform.localScale.z;
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }

    void VerticalMove(float dz, float roadX)
    {
        walkSpeed = 0.15f;
        DoMoveX(roadX);
        DoMoveZ(Mathf.Sign(dz));

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
        .DOMoveZ(transform.position.z + 20.0f * key, 1.5f)
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
