using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }
   
    public GameObject loadingScreen;
    public ProgressBar progressBar;

   
    void Awake()
    {
        Instance = this;

       SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        //  UiController = GetComponentInChildren<UiController>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public void LoadGame()
    {
        loadingScreen.gameObject.SetActive(true);
        scenesLoading.Add(SceneManager.UnloadSceneAsync(1));
        scenesLoading.Add(SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive));

        StartCoroutine(GetSceneLoadProgress());
        StartCoroutine(GetTotalProgress());
    }

    private float totalSceneProgress;
    private float totalLoadProgress;
    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
              while (!scenesLoading[i].isDone)
              {
                  totalSceneProgress = 0;
                  foreach (var operation in scenesLoading)
                  {
                      totalSceneProgress += operation.progress;
                  }
  
                  totalSceneProgress = (totalSceneProgress / scenesLoading.Count) *100f;
                    
                  yield return null;
              }
            
            
        }
    }

    public IEnumerator GetTotalProgress()
    {
        Debug.Log("Hello");
        float totalProgress = 0;
        while (SerializationManager.current == null || !SerializationManager.current.isDone)
        {
            if (SerializationManager.current == null)
            {
                totalLoadProgress = 0;
            }
            else
            {
              totalLoadProgress = Mathf.Round(SerializationManager.current.progress * 100);
            }
            totalProgress = Mathf.RoundToInt((totalSceneProgress + totalLoadProgress) / 2);
            progressBar.current = Mathf.RoundToInt(totalProgress);
            yield return null;
        }
            
            loadingScreen.gameObject.SetActive(false);
        
    }

    // public void saveData()
    // {
    //     SerializationManager.current.Save();
    //
    // }
    public void ToMenuScene()
    {
        SceneManager.LoadScene(1,LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(2);
    }
}
