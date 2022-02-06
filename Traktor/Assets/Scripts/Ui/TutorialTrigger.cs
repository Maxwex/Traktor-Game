using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialTrigger : MonoBehaviour
{
    public string title;
    public string message;
    public bool triggerOnEnable;

    public UnityEvent ONContinueEvent;
    public UnityEvent ONDeclineEvent;

    private void OnEnable()
    {
        if (!triggerOnEnable)  { return; }

        Action continueCallback = null;
        Action declineCallback = null;

        if (ONContinueEvent.GetPersistentEventCount() > 0)
        {
            continueCallback = ONContinueEvent.Invoke;
        }
        Debug.Log(ONDeclineEvent.GetPersistentEventCount());
        if (ONDeclineEvent.GetPersistentEventCount() > 0)
        {
                declineCallback = ONDeclineEvent.Invoke;
                
        }
        UiController.instance.modalWindow.ShowAsHero(title,message, continueCallback, declineCallback);
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
