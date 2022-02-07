using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GasStation : MonoBehaviour
{
    private Vehicle vehicle;
    private Timer _timer  = new Timer(0);
    public Pricelist _pricelist;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Vehicle") || _timer.RemainingSeconds > 0) return;
        Debug.Log(999);

        
        vehicle = other.gameObject.GetComponentInChildren<Vehicle>();
        var amount = vehicle.missingFuel();
        UiController.instance.modalWindow.ShowQuery("Tankstelle", "Den Traktor für " + CalculateCost() + "€ voll Tanken", Refuel);
        
        _timer = new Timer(5);
    }

    private int CalculateCost()
    {
        return (int) (vehicle.missingFuel()*_pricelist.FuelPrice);
    }

    public void Refuel()
    {
        
        Playerdata.instance.bankAccount.Pay(CalculateCost());
        vehicle.refuel();
    }

    private void Update()
    {
        _timer.Tick(Time.deltaTime);
    }
}
