using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBoxList: MonoBehaviour
{
    public TargetBox CowTargetBox;
    public TargetBox CheeseTargetBox;
    public TargetBox SiloTargetBox;
    public TargetBox AnimalTargetBox;
    public TargetBox ChickedTargetBox;
    public TargetBox GrowhouseTargetBox;
    public TargetBox SiloBigTargetBox;
    public TargetBox SupermarketTargetBox;
    
    public static TargetBoxList instance;

    private void Awake()
    {
        instance = this;
    }
}
