using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mission
{
    
    private Item item;
    private TrailerTypes.TrailerType RequiredTrailerType;
    private TargetBox startPoint;
    private TargetBox endPoint;
    private MissionType type;
    private float _amount;

    private TabGroup _tabGroup;
    public enum MissionType
    {
        Sell, Buy
    }

    private bool _collected;
    public static Action delivered;
    public static Action collected;
    public Mission(Item item, float amount, TrailerTypes.TrailerType requiredTrailerType, TargetBox startPoint, TargetBox endPoint, MissionType type)
    {
        RequiredTrailerType = requiredTrailerType;
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        this.item = item;
        this.type = type;
        this._amount = amount;
        
    }

    public void Start()
    {
        _tabGroup = UiController.instance.TabGroup;
        UiController.instance.notebook.deactivateButtons();
        _tabGroup.CloseWindow();
        startPoint.gameObject.SetActive(true);
        TargetBox.onCollision += CheckCollision;
        UiController.instance.modalWindow.ShowAsPromt("", "Bringe " + TrailerTypes.names(RequiredTrailerType) + " " +  endPoint.artikle + " " + startPoint.name + ".") ;
    }

    private void CheckCollision(Collider collider)
    {
        if (collider.GetComponent<Trailer>() == null) return;

        if (collider.GetComponent<Trailer>().TrailerType == RequiredTrailerType)
        {

            if (_collected)
            {
                Deliver();
                UiController.instance.notebook.activateButtons();
                SoundManager.current.Play("Pling");
            }
            else
            {
                Collect();
                SoundManager.current.Play("Pling");
            }
        }
     
    }

    private void Collect()
    { 
        UpdateTargets();
        UiController.instance.modalWindow.ShowAsPromt("", item.name + " wurde eingeladen. Bring die Ladung " + endPoint.artikle + " " + endPoint.name + ".");
       
        if (type == MissionType.Buy)
        {
            var value = (int)(_amount * item.price);
            Playerdata.instance.bankAccount.Pay(value);
        }
        else
        {
            item.value -= _amount;
        }
        _collected = true;
        collected?.Invoke();
       
    }
    private void Deliver()
    {
        UiController.instance.modalWindow.ShowAsPromt("",  item.name + " wurde abgeliefert.");
        if (type == MissionType.Sell)
        {
            Playerdata.instance.bankAccount.Receive((int) (_amount * item.price));
        }
        else
        {
            item.value += _amount;
        }
        TargetBox.onCollision -= CheckCollision;
        endPoint.gameObject.SetActive(false);
        delivered?.Invoke();

    }
    
    public void UpdateTargets()
    {
        startPoint.gameObject.SetActive(false);
        endPoint.gameObject.SetActive(true);
        
    }
}
