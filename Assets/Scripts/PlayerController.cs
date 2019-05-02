using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    float walkSpeed = 0.5f;

    public void Init()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal") * walkSpeed;
        float dz = Input.GetAxis("Vertical") * walkSpeed;
        transform.Translate(dx, 0, dz);
    }
}
