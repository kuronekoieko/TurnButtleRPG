using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] CameraController cameraController;
    [SerializeField] MemberController[] memberControllers;

    [SerializeField] EnemyController enemyController;


    // Start is called before the first frame update
    void Start()
    {
        playerController.Init();
        cameraController.Init(playerController);
        Params.gameMode = GameMode.WALK;
    }

    void FixedUpdate()
    {
        switch (Params.gameMode)
        {
            case GameMode.WALK:
                cameraController.FollowPlayer();
                playerController.WalkRoad();
                memberControllers[0].FollowTarget();
                memberControllers[1].FollowTarget();
                memberControllers[2].FollowTarget();
                break;
            case GameMode.BUTTLE_START:
                cameraController.ButtleStartCam();
                ChangeRotate();

                Params.gameMode = GameMode.CAM_MOVE;
                break;
            case GameMode.CAM_MOVE:
                break;
            case GameMode.CAM_MOVE_COMPLETED:
                MoveButllePos(cameraController.centerX);
                Params.gameMode = GameMode.ENEMY_APPEAR;
                break;
            case GameMode.ENEMY_APPEAR:
                enemyController.gameObject.SetActive(true);
                enemyController.Init();
                enemyController.transform.position = new Vector3(cameraController.centerX - 6, 1, 0);
                Params.gameMode = GameMode.BUTTLE;
                break;
            case GameMode.BUTTLE:
                break;
            case GameMode.RESULT:
                break;
            case GameMode.BUTTLE_END:
                break;

            default:
                break;
        }

    }

    void MoveButllePos(float centerX)
    {
        //        Debug.Log(centerX);
        Vector3 pos = new Vector3(centerX + 6, 1, 4);
        playerController.MoveButllePos(pos);
        pos += new Vector3(1, 0, -3);
        memberControllers[0].MoveButllePos(pos);
        pos += new Vector3(1, 0, -3);
        memberControllers[1].MoveButllePos(pos);
        pos += new Vector3(1, 0, -3);
        memberControllers[2].MoveButllePos(pos);
    }

    void ChangeRotate()
    {
        playerController.transform.rotation = Quaternion.Euler(Constants.BUTTLE_DEG, 0, 0);
        memberControllers[0].transform.rotation = Quaternion.Euler(Constants.BUTTLE_DEG, 0, 0);
        memberControllers[1].transform.rotation = Quaternion.Euler(Constants.BUTTLE_DEG, 0, 0);
        memberControllers[2].transform.rotation = Quaternion.Euler(Constants.BUTTLE_DEG, 0, 0);


    }

}