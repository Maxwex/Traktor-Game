using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public Button ContinueButton;
    private static string path;
    private void Awake()
    {
        path = Application.persistentDataPath + "/saves/gamedata.save";
        if (!File.Exists(path))
        {
            ContinueButton.interactable = false;
        }
        else
        {
            ContinueButton.interactable = true;
        }
    }

    public void Play()
    {
        GameManager.Instance.LoadGame();
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // if (SerialisationManager.SaveExists())
        // {
        //     Debug.Log("ja");
        //     SerialisationManager.Load();
        // }
    }

    public void DeleteSave()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}
