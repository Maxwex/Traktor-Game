using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController Instance;
    [SerializeField] private string inputSteerAxis = "Horizontal";
    [SerializeField] private string inputThrottleAxis = "Vertical";
    [SerializeField] private string InputCoppleButton = "Fire1";
    
    public float ThrottleInput { get; private set; }
    public float SteerInput { get; private set; }
    public float LooseInput { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SteerInput = Input.GetAxis(inputSteerAxis);
        ThrottleInput = Input.GetAxis(inputThrottleAxis);
        LooseInput = Input.GetAxis(InputCoppleButton);
    }
}
