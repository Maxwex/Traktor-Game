using System;
using System.Collections;
using System.Collections.Generic;
using Ui;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public static UiController instance;

    [SerializeField] private ModalWindowPanel _modalWindow;
    [SerializeField] private TabGroup _tabGroup;
    [SerializeField] private Notebook _notebook;
    
    public Button notbookButton;

    public ModalWindowPanel modalWindow => _modalWindow;
    public TabGroup TabGroup => _tabGroup;
    
    public GameObject optionSelector;

    public Notebook notebook => _notebook;

    private void Awake()
    {
        instance = this;
    }

    public void ShowOptions()
    {
        optionSelector.SetActive(true);
    }
    
    public void CloseOptions()
    {
        optionSelector.SetActive(false);
    }



    public void UpdateMoney()
    {
        throw new NotImplementedException();
    }
}
