using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBox : MonoBehaviour
{
    public String name;
    public String artikle;

    public static event Action<Collider> onCollision;
    private void OnTriggerEnter(Collider other)
    {
        onCollision?.Invoke(other);
    }
}
