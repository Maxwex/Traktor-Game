using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearProgressBar()
    {
        GameObject gameObject = Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"), Selection.activeGameObject.transform, false);
    }
    
    [MenuItem("GameObject/UI/Radial Progress Bar")]
    public static void AddRadialProgressBar()
    {
        GameObject gameObject = Instantiate(Resources.Load<GameObject>("UI/Radial Progress Bar"), Selection.activeGameObject.transform, false);
    }
#endif
    
    public int minimum;
    public int maximum;
    public Image Mask;
    public int current;
    public Image fill;
    public Color color;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void GetCurrentFill(){
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;
        Mask.fillAmount = fillAmount;

        fill.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }
}
