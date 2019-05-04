using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerController playerController;
    Vector3 playerToCamVec;
    public void Init()
    {
        playerController = SerializeManager.i.playerController;
        playerToCamVec = transform.position - playerController.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Clamp(playerToCamVec.x + playerController.dx / 3, -6, 6);
        playerToCamVec = new Vector3(x, playerToCamVec.y, playerToCamVec.z);
        transform.position = playerController.transform.position + playerToCamVec;
    }
}
