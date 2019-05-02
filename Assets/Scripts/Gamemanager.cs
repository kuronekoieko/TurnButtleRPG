using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        SerializeManager.i.playerController.Init();
        SerializeManager.i.cameraController.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

}