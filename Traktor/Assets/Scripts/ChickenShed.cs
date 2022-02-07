using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenShed : MonoBehaviour, ISaveable
{
     public static event Action<float,float,float> updateUi;
     
    private Mission _mission;
    
    private int TransactionValue;
    [SerializeField] private Item chicken;
    [SerializeField] private Item eggs;
    [SerializeField] private Item _food;
    
    [SerializeField]private float eggFactor;

    private void Start()
    {
        GameClock.instance.OnPassedInterval += PassedInterval;
        UpdateNotebook();
    }

    private void PassedInterval()
    {
        ProduceEggs();
        UpdateNotebook();
    }
    // Update is called once per frame
    private void UpdateNotebook()
    {
         updateUi?.Invoke(chicken.value,_food.value,eggs.value);
    }
    
    public void SellEggs()
    {
        
        _mission = new Mission(eggs,eggs.value, TrailerTypes.TrailerType.FoodTrailer,TargetBoxList.instance.ChickedTargetBox,TargetBoxList.instance.SupermarketTargetBox,Mission.MissionType.Sell);
        UiController.instance.modalWindow.ShowQuery("Bestätigung", "Die gesamten Eier für "+ eggs.value*eggs.price+" € verkaufen", _mission.Start);
    }
    
    public void BuyChicken()
    {
        if (Playerdata.instance.bankAccount.CanPay((int) (10*chicken.price)))
        {
        _mission = new Mission(chicken,10, TrailerTypes.TrailerType.FoodTrailer,TargetBoxList.instance.AnimalTargetBox,TargetBoxList.instance.ChickedTargetBox,Mission.MissionType.Buy);
        UiController.instance.modalWindow.ShowQuery("Bestätigung", "10 Hühner für "+ 10*chicken.price+" € kaufen", _mission.Start);
    }
        else
        {
            UiController.instance.modalWindow.ShowAsPromt("", "Du hast nicht genug Geld");
        }
    }

    public void buyFood()
    {
        var amount = _food.maxValue - _food.value;
        if (Playerdata.instance.bankAccount.CanPay((int) (amount*_food.price)))
        {
            _mission = new Mission(_food,amount, TrailerTypes.TrailerType.FoodTrailer,TargetBoxList.instance.SiloTargetBox,TargetBoxList.instance.ChickedTargetBox,Mission.MissionType.Buy);
        UiController.instance.modalWindow.ShowQuery("Bestätigung", "Hühnerfutter für "+ amount*_food.price+" € kaufen", _mission.Start);
        }
        else
        {
            UiController.instance.modalWindow.ShowAsPromt("", "Du hast nicht genug Geld");
        }
    }
    
    
    private void ProduceEggs()
    {
        if (_food.value <= 0 || eggs.value >= eggs.maxValue) return;
        
        var usage = chicken.value * eggFactor;

        if (_food.value < usage ) usage = _food.value;
 
        _food.value -= usage;
        eggs.value += usage / 2;
        if (eggs.value + usage / 2 > eggs.maxValue) eggs.value = eggs.maxValue;
       
    }
    
    public object CaptureState()
    {
        return new SaveData()
        {
            chicken = chicken.value,
            eggs = eggs.value,
            food = _food.value
        };

    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData) state;
        chicken.value = saveData.chicken;
        eggs.value = saveData.eggs;
        _food.value = saveData.food;
    }
    
    [Serializable]
    private struct SaveData
    { 
        public float chicken;
        public float eggs;
        public float food;
    }
}
