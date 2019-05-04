using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    PlayerController playerController;
    Vector3 playerToCamVec;
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

             });

        transform.DORotate(
          new Vector3(45f, 0, 0),   // 終了時点のRotation
          Constants.BUTTLE_START_SEC                    // アニメーション時間
      );
    }

}
