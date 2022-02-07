using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class ModalWindowPanel : MonoBehaviour
    {

        [SerializeField] private Transform modalWindowBox;

        [Header("Header")]
        [SerializeField] private Transform headerArea;
        [SerializeField] private TextMeshProUGUI titleField;
    
        [Header("Content")]
        [SerializeField] private Transform contentArea;
        [SerializeField] private TextMeshProUGUI contentField;
    
        [Header("Footer")]
        [SerializeField] private Transform footerArea;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Text confirmButtonText;
    
        [SerializeField] private Button declineButton;
        [SerializeField] private Text declineButtonText;

        private Action onConfirmAction;
        private Action onDeclineAction;

    
        public void Confirm()
        {Close();
            onConfirmAction?.Invoke();
            // 
        }
    
        public void Decline()
        {
            Close();
            onDeclineAction?.Invoke(); 
        }

        private void Close()
        {
            modalWindowBox.gameObject.SetActive(false);
            UiController.instance.notbookButton.interactable = true;
            Time.timeScale = 1;
        }

        private void Show()
        {
            modalWindowBox.gameObject.SetActive(true);
            UiController.instance.notbookButton.interactable = false;
            Time.timeScale = 0;

        }

        public void ShowAsHero(string title, string message, string confirmMessage, string declineMessage, Action confirmAction, Action declineAction = null)
        {
            Debug.Log("activated");Show ();
            bool hasTitle = string.IsNullOrEmpty(title);
            headerArea.gameObject.SetActive(!hasTitle);
        
            titleField.text = title;

            contentField.text = message;

            onConfirmAction = confirmAction;
            confirmButtonText.text = confirmMessage;

            bool hasDecline = (declineAction != null);
            declineButton.gameObject.SetActive(hasDecline);
            declineButtonText.text = declineMessage;
            onDeclineAction = declineAction;
       
       
        }

        public void ShowAsHero(string title, string message, Action confirmAction)
        {
            ShowAsHero(title, message, "Weiter", "", confirmAction);
        }
    
        public void ShowAsHero(string title, string message, Action confirmAction, Action declineAction)
        {
            ShowAsHero(title, message, "Weiter", "Zurück", confirmAction, declineAction);
        }

        public void ShowQuery(string title, string message, Action confirmAction)
        { 
            ShowAsHero(title, message, "Ja", "Nein", confirmAction,  Close);
  
        }


        public void ShowAsPromt(string title, string message)
        {
            Show();
            bool hasTitle = string.IsNullOrEmpty(title);
            headerArea.gameObject.SetActive(!hasTitle);
        
            titleField.text = title;

            contentField.text = message;
        
            confirmButtonText.text = "OK";
            onConfirmAction = Close;
        
            declineButton.gameObject.SetActive(false);
        }
    }
}
