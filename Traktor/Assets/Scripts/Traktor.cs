using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traktor : MonoBehaviour
{
    public Transform centerOfMass;
    
    //left to right, front to back
    public GameObject[] wheels;

    public float motorTorque = 100f;
    public float maxSteer = 20f;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }
    void FixedUpdate()
    {
        for (int i = 2; i < 4; i++)
        {
            wheels[i].GetComponent<WheelCollider>().motorTorque = Input.GetAxis("Vertical") * motorTorque;
        }
        
        for (int i = 0; i < 2; i++)
        {
            wheels[i].GetComponent<WheelCollider>().steerAngle = Input.GetAxis("Horizontal") * maxSteer;
        }
    }

    void Update()
    {
        var pos = Vector3.zero;
        var rot = Quaternion.identity;
        
        foreach (GameObject wheel in wheels)
        {
            wheel.GetComponent<WheelCollider>().GetWorldPose(out pos,out rot);
            wheel.transform.position = pos;
            wheel.transform.rotation = rot;
        }
       
    }
}
