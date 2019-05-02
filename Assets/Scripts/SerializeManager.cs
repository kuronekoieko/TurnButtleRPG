using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializeManager : MonoBehaviour
{
    public static SerializeManager i;
    void OnEnable()
    {
        i = this;
    }

    [SerializeField] PlayerController _playerController;
    [SerializeField] CameraController _cameraController;

    public PlayerController playerController
    {
        get { return _playerController; }
    }

    public CameraController cameraController
    {
        get { return _cameraController; }
    }
}
