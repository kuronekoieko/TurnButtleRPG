using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PartyManager : MonoBehaviour
{
    [NonSerialized] public PartyController[] partyControllers;
    PlayerController playerController;

    public void Init()
    {
        partyControllers = new PartyController[transform.childCount];
        for (int i = 0; i < partyControllers.Length; i++)
        {
            partyControllers[i] = transform.GetChild(i).GetComponent<PartyController>();
        }
    }

    public void Walk()
    {
        for (int i = 0; i < partyControllers.Length; i++)
        {
            partyControllers[i].Walk();
        }
    }

    public void MoveWalkPosStart()
    {
        for (int i = 0; i < partyControllers.Length; i++)
        {
            partyControllers[i].MoveBeforeBattlePos();
        }
    }

    public void MoveBattlePos(CameraController cameraController)
    {

        Vector3 pos = new Vector3(cameraController.centerX + 6, 1, cameraController.centerZ + 4);

        for (int i = 0; i < partyControllers.Length; i++)
        {
            partyControllers[i].MoveButllePos(pos);
            pos += new Vector3(1, 0, -3);
        }
    }

    public void ChangeRotate(float rotationX)
    {
        for (int i = 0; i < partyControllers.Length; i++)
        {
            partyControllers[i].SetRotate(rotationX);
        }

    }
}
