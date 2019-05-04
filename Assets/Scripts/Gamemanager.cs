using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] CameraController cameraController;
    [SerializeField] MemberController[] memberControllers;


    // Start is called before the first frame update
    void Start()
    {
        playerController.Init();
        cameraController.Init(playerController);
        Params.gameMode = GameMode.WALK;
    }

    // Update is called once per frame
    void Update()
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
                Params.gameMode = GameMode.BUTTLE;
                break;
            default:
                break;
        }
    }

}