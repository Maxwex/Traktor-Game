using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButton;
    public Color tabIdle;
    public Color tabHover;
    public Color tabSelected;
    public TabButton selectedTab;
    public List<GameObject> objectsToSwap;

    public GameObject TabParent;
    public Button closeButton;
    public Button notebookButton;

    private void Awake()
    {
        notebookButton = UiController.instance.notbookButton;
    }

    public void Subscribe(TabButton Button)
    {
        if (tabButton == null)
        {
            tabButton = new List<TabButton>();
        }
        
        tabButton.Add(Button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.color = tabHover;

        }
    }
    
    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }
    
    public void OnTabSelected(TabButton button)
    {
        selectedTab = button;
        ResetTabs();
        button.background.color = tabSelected;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (var button in tabButton)
        {
            if (selectedTab!=null && button == selectedTab)
            {
                continue;
            }
            button.background.color = tabIdle;
        }
    }

    public void CloseWindow()
    {
        TabParent.SetActive(false);
        notebookButton.interactable = true;
    }

    public void OpenWindow()
    {
        TabParent.SetActive(true);
        notebookButton.interactable = false;
    }
}
