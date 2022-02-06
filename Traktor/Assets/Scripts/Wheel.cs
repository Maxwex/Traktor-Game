using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private bool steer;    
    [SerializeField] private bool power;

    public float SteerAngle { get; set; }
    public float Torque { get; set; }
    public float Brake { get; set; }

    private WheelCollider _wheelCollider;
    private Transform _wheelTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        _wheelCollider = GetComponentInChildren<WheelCollider>();
        _wheelTransform = GetComponentInChildren<MeshRenderer>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _wheelCollider.GetWorldPose(out Vector3 pos,out Quaternion rot);
        _wheelTransform.position = pos;
        _wheelTransform.rotation = rot;
    }

    private void FixedUpdate()
    {
        if (steer)
        {
            _wheelCollider.steerAngle = SteerAngle;
        }
        
        if (power)
        {
            _wheelCollider.motorTorque = Torque;
        }

        _wheelCollider.brakeTorque = Brake;

    }
}
