using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

public class Trailer : MonoBehaviour, ISaveable
{

    private Rigidbody m_Rigidbody;
    private Collider jointCollider;
    private ConfigurableJoint _joint;
    private Wheel[] _wheels;
    [SerializeField] private float brakeTorque; 
    public TrailerTypes.TrailerType TrailerType;

    public Action onConnect;
    public Action onLoose;
    public float Loose { get;  set; }
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        _joint = GetComponent<ConfigurableJoint>();
        jointCollider = GetComponentInChildren<SphereCollider>();
        _wheels = GetComponentsInChildren<Wheel>();
        Debug.Log(gameObject.transform.position);
    }

    public bool connected()
    {
        return _joint.connectedBody != null;
    }
    private void Update()
    {
        // if (m_Rigidbody.velocity.magnitude > 0.5f)
        // {
        //     m_Rigidbody.mass = 800;
        // }

        Loose = InputController.Instance.LooseInput;
        if (!(Loose > 0)) return;
        if (_joint.connectedBody != null)
        {
            _joint.connectedBody.GetComponentInParent<Rigidbody>().GetComponentInParent<Vehicle>().Trailer = null;
            
        }
        _joint.connectedBody = null;
    }
    


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Joint")) return;
        SoundManager.current.Play("TrailerConnect");
        Vector3 target = ((Component) this).transform.InverseTransformPoint(other.gameObject.transform.position);
        Vector3 difference = target - _joint.anchor;
        gameObject.transform.Rotate(difference);
        gameObject.transform.Translate(difference);
        _joint.connectedBody = other.GetComponentInParent<Rigidbody>();
        other.GetComponentInParent<Vehicle>().Trailer = this;
        onConnect?.Invoke();
        
    }
    
    public void Brake()
    {
        foreach (var wheel in _wheels)
        {
            wheel.Brake = 0;
        }
    }

    public object CaptureState()
    {
        Debug.Log(transform.position);
        return new SaveData()
        {
            position = gameObject.transform.position,
            rotation = gameObject.transform.rotation
        };

        
    }

    public void RestoreState(object state)
    {
        gameObject.SetActive(false);
        var saveData = (SaveData) state;
        gameObject.transform.SetPositionAndRotation(saveData.position, saveData.rotation);

        Debug.Log(gameObject.transform.position);
        gameObject.SetActive(true);
    }
    
    [Serializable]
    private struct SaveData
    {
        public Vector3 position;
        public Quaternion rotation;
    }
}


