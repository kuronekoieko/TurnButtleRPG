using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRoadProperty : RoadController
{
    [SerializeField] float _stopPointOffset;
    public float stopPoint
    {
        get { return transform.position.x + _stopPointOffset; }
    }
}
