using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank
{
    public event Action<int> MoneyChanged;
    public int Money { get; private set; }

    public Bank(int money)
    {
        Money = 0;
    }
    
    public bool CanPay(int amount)
    {
        return Money >= amount;
    }
    
    public void Pay(int amount)
    {
        Money -= amount;
        MoneyChanged?.Invoke(Money);
    }
    public void Set(int amount)
    {
        Money = amount;
        MoneyChanged?.Invoke(Money);
    }
    public void Receive(int amount)
    {
        Money += amount;
        MoneyChanged?.Invoke(Money);

    }
    
}
