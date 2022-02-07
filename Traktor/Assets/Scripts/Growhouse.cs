using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growhouse : MonoBehaviour, ISaveable
{
    public static event Action<float> updateUi;
    
    private Mission _mission;

    public Item tomatoes;
    [SerializeField] private float growmodifier;
    private void Start()
    {
        GameClock.instance.OnPassedInterval += PassedInterval;
        UpdateNotebook();
    }

    private void PassedInterval()
    {
        ProduceTomatoes();
        UpdateNotebook();
    }

    private void ProduceTomatoes()
    {
        if (tomatoes.value >= tomatoes.maxValue) return;
        tomatoes.value += growmodifier;
    }

    // Update is called once per frame
    public void SellTomato()
    {
        if (tomatoes.value < tomatoes.maxValue)
        {
            UiController.instance.modalWindow.ShowAsPromt("", "Die Tomaten sind noch nicht reif.");
            return;
        }
        _mission = new Mission(tomatoes, tomatoes.value, TrailerTypes.TrailerType.FoodTrailer,TargetBoxList.instance.GrowhouseTargetBox,TargetBoxList.instance.SupermarketTargetBox,Mission.MissionType.Sell);
        UiController.instance.modalWindow.ShowQuery("Bestätigung", "Die Tomaten für "+ tomatoes.value*tomatoes.price+" € verkaufen", _mission.Start);
    }
    
    
    private void UpdateNotebook()
    {
        updateUi?.Invoke(tomatoes.value/tomatoes.maxValue*100);
    }
    
    public object CaptureState()
    {
        return new SaveData()
        {
            tomatoes = tomatoes.value,
        };

    }

    public void RestoreState(object state)
    {
        var saveData = (SaveData) state;
        tomatoes.value = saveData.tomatoes;
    }
    
    [Serializable]
    private struct SaveData
    {
        public float tomatoes;
    }
}
