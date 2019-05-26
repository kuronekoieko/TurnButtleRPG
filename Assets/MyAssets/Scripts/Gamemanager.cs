using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class GameManager : MonoBehaviour
{

    [SerializeField] CameraController cameraController;
    [SerializeField] EnemyController enemyController;
    [SerializeField] BattleManager BattleManager;
    [SerializeField] DataManager dataManager;
    [SerializeField] PartyManager partyManager;

    float time;



    void Start()
    {
        //prayerPrefabをシングルトン化
        dataManager.Init();
        //partyManagerを初期化
        partyManager.Init();
        //カメラコントローラーを初期化
        cameraController.Init(partyManager.playerController);
        Params.gameMode = GameMode.WALK;

        enemyController.Init();

        BattleManager.Init();
    }

    void FixedUpdate()
    {
        switch (Params.gameMode)
        {
            case GameMode.WALK:
                //カメラの追従処理
                cameraController.FollowPlayer();
                partyManager.Walk();

                EnemyEncounter();

                break;
            case GameMode.CAM_MOVE_UP_START:
                partyManager.playerController.WalkFinalize();
                cameraController.MoveBattleStartCam();

                Params.gameMode = GameMode.CAM_MOVE_UP;
                break;
            case GameMode.CAM_MOVE_UP:

                break;
            case GameMode.CAM_MOVE_UP_COMPLETED:
                cameraController.GetCenter();
                partyManager.MoveBattlePos(cameraController);
                Params.gameMode = GameMode.ENEMY_APPEAR;
                break;
            case GameMode.MEMBER_MOVE_BATTLE_POS_START:
                break;
            case GameMode.MEMBER_MOVE_BATTLE_POS:
                break;
            case GameMode.MEMBER_MOVE_BATTLE_POS_COMPLETED:
                break;


            case GameMode.ENEMY_APPEAR:
                enemyController.gameObject.SetActive(true);

                enemyController.transform.position = new Vector3(cameraController.centerX - 6, 1, cameraController.centerZ + 0);
                BattleManager.BattleInitialize();
                Params.gameMode = GameMode.BATTLE;
                break;
            case GameMode.BATTLE:
                BattleManager.BattleUpdate();
                break;
            case GameMode.RESULT:
                break;

            case GameMode.BATTLE_END:
                BattleManager.BattleFinalize();
                enemyController.gameObject.SetActive(false);
                Params.gameMode = GameMode.MEMBER_MOVE_WALK_POS_START;
                break;

            case GameMode.MEMBER_MOVE_WALK_POS_START:
                partyManager.MoveWalkPosStart();
                Params.gameMode = GameMode.MEMBER_MOVE_WALK_POS;
                break;
            case GameMode.MEMBER_MOVE_WALK_POS:
                break;
            case GameMode.MEMBER_MOVE_WALK_POS_COMPLETED:
                Params.gameMode = GameMode.CAM_MOVE_DOWN_START;
                break;

            case GameMode.CAM_MOVE_DOWN_START:
                cameraController.MoveBattleEndCam();
                Params.gameMode = GameMode.CAM_MOVE_DOWN;
                break;
            case GameMode.CAM_MOVE_DOWN:
                break;
            case GameMode.CAM_MOVE_DOWN_COMPLETED:

                Params.gameMode = GameMode.WALK;
                break;
            default:
                break;
        }
        partyManager.SetRotate(cameraController.transform.localEulerAngles.x);
        enemyController.SetRotate(cameraController.transform.localEulerAngles.x);
    }

    void EnemyEncounter()
    {
        time += Time.fixedDeltaTime;
        if (time > 5)
        {
            Params.gameMode = GameMode.CAM_MOVE_UP_START;
            time = 0;
        }
    }



}