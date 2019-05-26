using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public void Init()
    {
        transform.localScale = new Vector3(-1, 1, 1);
    }



    public void SetRotate(float rotationX)
    {
        //
        transform.rotation = Quaternion.Euler(rotationX, 0, 0);
    }

}
