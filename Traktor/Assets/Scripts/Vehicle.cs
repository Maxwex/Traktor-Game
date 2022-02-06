using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Vehicle : MonoBehaviour, ISaveable
{
    public Transform centerOfMass;

    public event Action<int> FuelChanged;
    
    private Wheel[] wheels;

    [SerializeField] private float motorTorque;
    [SerializeField] private float brakeTorque;
    [SerializeField] private float fuel;
    [SerializeField] private float maxFuel;
    [SerializeField] private float fuelUsage;

    private AudioSource motor;
    public float maxSteer = 20f;
    public float maxSpeed = 6f;
    private Rigidbody _rigidbody;
    private bool _IsReversing;

    
    public float Throttle { get;  set; }
    public float Steer { get;  set; }
    
    public Trailer Trailer { get;  set; }
    
// Start is called before the first frame update
    void Awake()
    {
        Sound s;
        SoundManager.current.SoundDict.TryGetValue("Motor", out s);
        motor = s.Source;
        
        motor.Play();
        fuel = maxFuel;
        wheels = GetComponentsInChildren<Wheel>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
        Playerdata.instance.ActiVehicle = this;
    }

    // Update is called once per frame
    void Update()
    {
         Steering();
         UseFuel();
         UpdateUi();
         PlaySound();
    }

    private void PlaySound()
    {
        
        motor.pitch = (_rigidbody.velocity.magnitude)/3+1;
    }

    private void Steering()
    {
        if (fuel == 0)
        {
            return;
        }

        if (_rigidbody.velocity.magnitude>= maxSpeed)
        {
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxSpeed); 
        }
        _IsReversing = transform.InverseTransformDirection(_rigidbody.velocity).z < 0f;
        Steer = InputController.Instance.SteerInput;
        Throttle = InputController.Instance.ThrottleInput;
        foreach (var wheel in wheels)
        {      
            wheel.SteerAngle = Steer * maxSteer;
        }                        
        if (Throttle != 0.0f)
        {
            if (_IsReversing)
            {
                if (Throttle > 0)
                {
                    Brake();
                }
                else
                {
                    Drive();
                }
            }
            else
            {
                if (Throttle < 0)
                {
                    Brake();
                }
                else
                {
                    Drive();
                }
            }
        }

    }
    private void Brake()
    {
        foreach (var wheel in wheels)
        {
             wheel.Brake = Mathf.Abs(Throttle) * brakeTorque;
             wheel.Torque = 0;
        }

        if (Trailer != null)
        {
            Trailer.Brake();
        }
    }
    private void Drive()
    {
        foreach (var wheel in wheels)
        {
            wheel.Torque = Throttle * motorTorque;
            wheel.Brake = 0;
        }
    }
    
    private int FuelPercentage()
    {
        return (int) Mathf.Ceil((fuel / maxFuel) * 100);
    }
    
    private void UseFuel()
    {
        var usage = Trailer!= null ? 2 * fuelUsage : fuelUsage;
        fuel -=  _rigidbody.velocity.magnitude* usage * Time.deltaTime;
        if (fuel < 0)
        {
            fuel = 0;
        }
     
    }

    private void OnCollisionEnter(Collision other)
    {
      
        if (other.gameObject.GetComponent<Trailer>()!=null && other.gameObject.GetComponent<Trailer>().connected())
        {
            
        }else  SoundManager.current.Play("Collision");
    }

    private void UpdateUi()
    {
        FuelChanged?.Invoke(FuelPercentage());
    }

    public int missingFuel()
    {
        return (int) (maxFuel - fuel);
    }

    public void refuel()
    {
        fuel = maxFuel;
    }

    public object CaptureState()
    {
        return new SaveData()
        {
            fuellevel = fuel,
            position = gameObject.transform.position,
            rotation = gameObject.transform.rotation
        };

    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData) state;
        fuel = saveData.fuellevel;
        gameObject.transform.SetPositionAndRotation(saveData.position, saveData.rotation);
    }
    
    [Serializable]
    private struct SaveData
    { 
        public Vector3 position;
        public Quaternion rotation;
        public float fuellevel;
    }
}
