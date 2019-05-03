using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    [SerializeField] private RoadType _roadType;
    public RoadType roadType
    {
        get { return _roadType; }
    }
    void Init()
    {

    }
}
