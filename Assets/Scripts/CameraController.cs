using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CameraController : MonoBehaviour
{
    PlayerController playerController;
    Vector3 playerToCamVec;

    [NonSerialized] public float centerX;
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

    public void ButtleStartCam()
    {

        float y = transform.position.y + 4;
        float z = transform.position.z + 6;

        Vector3 pos = new Vector3(transform.position.x, y, z);
        transform
             .DOMove(pos, Constants.BUTTLE_START_SEC)
             .SetEase(Ease.InOutSine)
             .OnComplete(() =>
             {
                 centerX = GetCenterX();
                 Params.gameMode = GameMode.CAM_MOVE_COMPLETED;
             });

        transform.DORotate(
          new Vector3(Constants.BUTTLE_DEG, 0, 0),   // 終了時点のRotation
          Constants.BUTTLE_START_SEC                    // アニメーション時間
      );
    }

    float GetCenterX()
    {

        //Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
        Ray ray = new Ray(transform.position, new Vector3(0, -10, 10));

        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit hit;

        //Rayの飛ばせる距離
        int distance = 20;

        //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * distance, Color.red);
        float x = 0;
        //もしRayにオブジェクトが衝突したら
        //                  ↓Ray  ↓Rayが当たったオブジェクト ↓距離
        if (Physics.Raycast(ray, out hit, distance))
        {
            // Debug.Log(hit.transform.position);
            x = hit.transform.position.x;
        }

        return x;
    }

}
