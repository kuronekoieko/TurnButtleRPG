using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class GameManager : MonoBehaviour
{
    PlayerController playerController;
    [SerializeField] CameraController cameraController;
    [SerializeField] PartyController[] partyControllers;

    [SerializeField] EnemyController enemyController;

    [SerializeField] RectTransform buttleUI;
    [SerializeField] ButtleManager buttleManager;
    float time;


    // Start is called before the first frame update
    void Start()
    {
        playerController = partyControllers[0].GetComponent<PlayerController>();
        playerController.Init();
        cameraController.Init(playerController);
        Params.gameMode = GameMode.WALK;
        buttleUI.gameObject.SetActive(false);
        for (int i = 0; i < partyControllers.Length; i++)
        {
            partyControllers[i].SetCameraController(cameraController);
        }
        enemyController.Init();
        enemyController.SetCameraController(cameraController);

        buttleManager.Init();
    }

    void FixedUpdate()
    {
        switch (Params.gameMode)
        {
            case GameMode.WALK:
                cameraController.FollowPlayer();
                for (int i = 0; i < partyControllers.Length; i++)
                {
                    partyControllers[i].Walk();
                }

                time += Time.fixedDeltaTime;
                if (time > 1)
                {
                    Params.gameMode = GameMode.BUTTLE_START;
                    time = 0;
                }

                break;
            case GameMode.BUTTLE_START:
                cameraController.MoveButtleStartCam();
                //ChangeRotate();

                Params.gameMode = GameMode.CAM_MOVE_UP;
                break;
            case GameMode.CAM_MOVE_UP:

                break;
            case GameMode.CAM_MOVE_UP_COMPLETED:
                cameraController.GetCenter();
                MoveButllePos();
                Params.gameMode = GameMode.ENEMY_APPEAR;
                break;
            case GameMode.ENEMY_APPEAR:
                enemyController.gameObject.SetActive(true);

                enemyController.transform.position = new Vector3(cameraController.centerX - 6, 1, cameraController.centerZ + 0);
                buttleUI.gameObject.SetActive(true);
                Params.gameMode = GameMode.BUTTLE;
                break;
            case GameMode.BUTTLE:
                buttleManager.ButtleUpdate();
                break;
            case GameMode.RESULT:
                break;
            case GameMode.BUTTLE_END:
                buttleUI.gameObject.SetActive(false);
                enemyController.gameObject.SetActive(false);
                for (int i = 0; i < partyControllers.Length; i++)
                {
                    partyControllers[i].MoveBeforeButllePos();
                }
                Params.gameMode = GameMode.CAM_MOVE_DOWN;
                break;
            case GameMode.CAM_MOVE_DOWN:

                break;
            case GameMode.CAM_MOVE_DOWN_COMPLETED:
                cameraController.MoveButtleEndCam();
                Params.gameMode = GameMode.WALK;
                break;
            default:
                break;
        }
        ChangeRotate();
    }

    void MoveButllePos()
    {

        Vector3 pos = new Vector3(cameraController.centerX + 6, 1, cameraController.centerZ + 4);

        for (int i = 0; i < partyControllers.Length; i++)
        {
            partyControllers[i].MoveButllePos(pos);
            pos += new Vector3(1, 0, -3);
        }
    }

    void ChangeRotate()
    {
        for (int i = 0; i < partyControllers.Length; i++)
        {
            partyControllers[i].SetRotate();
        }
        enemyController.SetRotate();
    }

}