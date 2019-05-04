using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void Init()
    {

        transform.rotation = Quaternion.Euler(Constants.BUTTLE_DEG, 0, 0);
        transform.localScale = new Vector3(-1, 1, 1);
    }


}
