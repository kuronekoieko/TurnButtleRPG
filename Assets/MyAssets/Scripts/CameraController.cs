using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CameraController : MonoBehaviour
{
    PlayerController playerController;
    Vector3 playerToCamVec;
    float camRotationXInWalk;

    [NonSerialized] public float centerX;
    [NonSerialized] public float centerZ;

    public void Init(PlayerController playerController)
    {
        this.playerController = playerController;
        playerToCamVec = transform.position - playerController.transform.position;
    }

    public void FollowPlayer()
    {
        float x = Mathf.Clamp(playerToCamVec.x + playerController.dx / 3, -6, 6);
        playerToCamVec = new Vector3(x, playerToCamVec.y, playerToCamVec.z);
        transform.position = playerController.transform.position + playerToCamVec;

    }

    public void MoveButtleStartCam()
    {
        camRotationXInWalk = transform.localEulerAngles.x;

        Vector3 pos = transform.position + Constants.BUTTLE_CAM_OFFSET;
        transform
             .DOMove(pos, Constants.BUTTLE_START_SEC)
             .SetEase(Ease.InOutSine)
             .OnComplete(() =>
             {
                 Params.gameMode = GameMode.CAM_MOVE_UP_COMPLETED;
             });

        transform.DORotate(
          new Vector3(Constants.BUTTLE_DEG, 0, 0),   // 終了時点のRotation
          Constants.BUTTLE_START_SEC                    // アニメーション時間
      );
    }

    public void MoveButtleEndCam()
    {
        Vector3 pos = transform.position - Constants.BUTTLE_CAM_OFFSET;
        transform
             .DOMove(pos, Constants.BUTTLE_START_SEC)
             .SetEase(Ease.InOutSine)
             .OnComplete(() =>
             {

             });

        transform.DORotate(
            new Vector3(camRotationXInWalk, 0, 0),   // 終了時点のRotation
            Constants.BUTTLE_START_SEC);                 // アニメーション時間


    }

    public void GetCenter()
    {
        // 自身の向きベクトル取得
        float angleDir = Constants.BUTTLE_DEG * (Mathf.PI / 180.0f);
        Vector3 dir = new Vector3(0, -Mathf.Sin(angleDir), Mathf.Cos(angleDir));

        //Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
        Ray ray = new Ray(transform.position, dir);

        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit hit;

        //Rayの飛ばせる距離
        int distance = 20;

        //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * distance, Color.red);
        centerZ = playerController.transform.position.z;
        centerX = playerController.transform.position.x;
        //もしRayにオブジェクトが衝突したら
        //                  ↓Ray  ↓Rayが当たったオブジェクト ↓距離
        if (Physics.Raycast(ray, out hit, distance))
        {
            centerX = hit.point.x;
            //centerZ = hit.collider.transform.position.z;
        }

    }


}
