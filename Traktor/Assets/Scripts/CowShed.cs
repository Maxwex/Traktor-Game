using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CowShed : MonoBehaviour,ISaveable
{

    public static event Action<float,float,float> updateUi;
    private Mission _mission;
    public Pricelist pricelist;
    private int TransactionValue;
    [SerializeField] private Item _cows;
    [SerializeField] private Item _milk;
    [SerializeField] private Item _food;
    
    [SerializeField]private float milkFactor;

    private void Start()
    {
        GameClock.instance.OnPassedInterval += PassedInterval;
        UpdateNotebook();
    }

    private void PassedInterval()
    {
        ProduceMilk();
        UpdateNotebook();
    }
    // Update is called once per frame
    private void UpdateNotebook()
    {
         updateUi?.Invoke(_cows.value,_food.value,_milk.value);
    }
    
    public void SellMilk()
    {
        
        _mission = new Mission(_milk,_milk.value, TrailerTypes.TrailerType.MilkTrailer,TargetBoxList.instance.CowTargetBox,TargetBoxList.instance.CheeseTargetBox,Mission.MissionType.Sell);
        UiController.instance.modalWindow.ShowQuery("Bestätigung", "Die gesamte Milch für "+ _milk.value*_milk.price+" € verkaufen", _mission.Start);
    }
    
    public void BuyCows()
    {
        if (Playerdata.instance.bankAccount.CanPay((int) (2*_cows.price)))
        {
            _mission = new Mission(_cows,2, TrailerTypes.TrailerType.CowTrailer,TargetBoxList.instance.AnimalTargetBox,TargetBoxList.instance.CowTargetBox,Mission.MissionType.Buy); 
            UiController.instance.modalWindow.ShowQuery("Bestätigung", "2 Kühe für "+ 2*_cows.price+" € kaufen", _mission.Start);
        }
        else
        {
            UiController.instance.modalWindow.ShowAsPromt("", "Du hast nicht genug Geld");
        }
        
        

    }

    public void buyFood()
    {
        var amount = _food.maxValue - _food.value;
        if (Playerdata.instance.bankAccount.CanPay((int) (amount*_food.price))){
        _mission = new Mission(_food,amount, TrailerTypes.TrailerType.FoodTrailer,TargetBoxList.instance.SiloBigTargetBox,TargetBoxList.instance.CowTargetBox,Mission.MissionType.Buy);
        UiController.instance.modalWindow.ShowQuery("Bestätigung", "Futter für "+ amount*_food.price+" € kaufen", _mission.Start);
        }
        else
        {
            UiController.instance.modalWindow.ShowAsPromt("", "Du hast nicht genug Geld");
        }
    }
    
    
    private void ProduceMilk()
    {
        if (_food.value <= 0 || _milk.value >= _milk.maxValue) return;
        
        var usage = _cows.value * milkFactor;

        if (_food.value < usage ) usage = _food.value;
 
        _food.value -= usage;
        _milk.value += usage / 2;
        if (_milk.value + usage / 2 > _milk.maxValue) _milk.value = _milk.maxValue;
       
    }
    
    public object CaptureState()
    {
        return new SaveData()
        {
            cows = _cows.value,
            milk = _milk.value,
            food = _food.value
        };

    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData) state;
        _cows.value = saveData.cows;
        _milk.value = saveData.milk;
        _food.value = saveData.food;
    }
    
    [Serializable]
    private struct SaveData
    { 
        public float cows;
        public float milk;
        public float food;
    }
}
