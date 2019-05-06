using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoadController : MonoBehaviour
{
    [SerializeField] private RoadType _roadType;
    public RoadType roadType
    {
        get { return _roadType; }
    }

    public StopRoadProperty stopProperty
    {
        get { return GetComponent<StopRoadProperty>(); }
    }


    public void Init()
    {

    }
}
