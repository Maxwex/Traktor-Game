using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClock : MonoBehaviour
{
    public static GameClock instance;
    
    public float interval;

    private float timePassed;
    
    public event Action OnPassedInterval;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed < interval) return;
        timePassed -= interval;
        if (OnPassedInterval != null) OnPassedInterval();
    }
}
