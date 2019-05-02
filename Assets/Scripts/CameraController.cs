using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player;
    Vector3 playerToCamSec;
    public void Init()
    {
        player = SerializeManager.i.playerController.gameObject.transform;
        playerToCamSec = transform.position - player.position;
        Debug.Log(playerToCamSec);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + playerToCamSec;
    }
}
