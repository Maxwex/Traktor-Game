using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerdata : MonoBehaviour, ISaveable
{
    public static Playerdata instance;

    public Vehicle ActiVehicle;

    public Bank bankAccount;

    private void Awake()
    {
        instance = this;
        bankAccount = new Bank(000);
       
    }

    private void Start()
    {
        bankAccount.Receive(5000);
    }

    public object CaptureState()
    {
        return new SaveData()
        {
            money = bankAccount.Money
        };
    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData) state;
        bankAccount.Set(saveData.money);
    }
    
    [Serializable]
    private struct SaveData
    {
        public int money;
    }
}
