using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Vector3 eulerRotation;
    public float damper;

    private Vector3 mouseOrigin;
    public Vector3 rotation;
    public float horizontalOffset;
    private float xOffset = 0;
    
    private bool pressed;
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        transform.eulerAngles = eulerRotation;
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(1))
        {
            mouseOrigin = Input.mousePosition;
            pressed = true;
            Debug.Log(66);
        }

        if (!Input.GetMouseButton(1))
        {
            pressed = false;
        }

        if (pressed)
        {
            xOffset = (Input.mousePosition - mouseOrigin).x;
            xOffset = xOffset / (Screen.width/2);
            offset = Quaternion.Euler(0, xOffset, 0) * offset;

            transform.rotation = Quaternion.Euler(0, xOffset, 0)*transform.rotation;
        }
        if (target != null)
         {
             transform.position = target.position + offset;
            
         }
    }

    
    
    private void OnMouseDrag()
    {
        
    }
}
