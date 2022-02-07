using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item")] 
public class Item : ScriptableObject
{
    public string name;
    public float maxValue;
    public float value;
    
    public float price;

}
