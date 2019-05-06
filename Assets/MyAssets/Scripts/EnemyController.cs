using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    CameraController cameraController;
    public void Init()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }


    public void SetCameraController(CameraController cameraController)
    {
        this.cameraController = cameraController;
    }
    public void SetRotate()
    {   
        transform.rotation = Quaternion.Euler(cameraController.transform.localEulerAngles.x, 0, 0);
    }

}
