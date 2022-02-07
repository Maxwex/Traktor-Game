using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notebook : MonoBehaviour
{
    public TextMeshProUGUI cows;
    public TextMeshProUGUI milk;
    public TextMeshProUGUI food;
    public TextMeshProUGUI chicken;
    public TextMeshProUGUI eggs;
    public TextMeshProUGUI chickenfood;
    public TextMeshProUGUI tomatoes;
    public Button[] Buttons;

    // Start is called before the first frame update
    void Start()
    {
        CowShed.updateUi += UpdateCowShed;
        ChickenShed.updateUi += UpdateChickenShed;
        Growhouse.updateUi += UpdateGrowhouse;
    }

    // Update is called once per frame
    void UpdateCowShed(float cows,float food, float milk)
    {
        this.cows.text = cows.ToString("0");
        this.food.text = food.ToString("0");
        this.milk.text = milk.ToString("0");
        
    }
    void UpdateChickenShed(float chicken,float food, float eggs)
    {
        this.chicken.text = chicken.ToString("0");
        chickenfood.text = food.ToString("0");
        this.eggs.text = eggs.ToString("0");
        
    }

    void UpdateGrowhouse(float tomato)
    {
        this.tomatoes.text = tomato + "%";
    }
   public void activateButtons()
    {
        foreach (var button in Buttons)
        {
            button.interactable = true;
        }
    }
    
   public void deactivateButtons()
    {
        foreach (var button in Buttons)
        {
            button.interactable = false;
        }
    }
}
