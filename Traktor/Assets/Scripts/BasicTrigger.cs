using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicTrigger : MonoBehaviour
{
    public UnityEvent TriggerEnter;
    public Vehicle vehicle;
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Vehicle")) { return;        }
        vehicle = other.gameObject.GetComponentInChildren<Vehicle>();
        TriggerEnter?.Invoke();
    }

    public void GETVehicle( out Vehicle vehicle)
    {
        vehicle = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
