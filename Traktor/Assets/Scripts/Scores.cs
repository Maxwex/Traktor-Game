using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    public Text FuelText;
    public Text Money;
    // Start is called before the first frame update
    private void Start()
    {
        Playerdata.instance.bankAccount.MoneyChanged += UpdateScore;
        Playerdata.instance.ActiVehicle.FuelChanged += UpdateFuel;
        

    }

    private void UpdateFuel(int amount)
    {
        FuelText.text = amount + "%";
    }

    private void UpdateScore(int money)
    {
        Money.text = money.ToString();
    }
}
