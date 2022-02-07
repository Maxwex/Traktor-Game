using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class SerializationManager : MonoBehaviour
{
    public static SerializationManager current; 
    private static string path;

    public float progress;
    public bool isDone;
    
    private void Awake()
    {
        current = this;
        path = Application.persistentDataPath + "/saves/gamedata.save";
    }

    private void Start()
    {
        Load();
    }

    public void Save()
    {
        var state = LoadFile();
        CaptureState(state);
        SaveFile(state);
        
    }

    public bool SaveExists()
    {
       return File.Exists(path);
    }
    
    public void Load()
    {
        Dictionary<string, object> state = LoadFile();
        StartCoroutine(RestoreState(state));
    }
    public void SaveFile(object state)
    {
        BinaryFormatter serializer = GetSerializer();
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }
        

        FileStream fileStream = File.Create(path);
        
        serializer.Serialize(fileStream, state);
        
        fileStream.Close();
        
    }

    public Dictionary<string, object> LoadFile()
    {
        if (!File.Exists(path))
        { 
            return new Dictionary<string,object>();
           
        }

     
        using (FileStream fileStream = File.Open(path, FileMode.Open))
            
        {       
               BinaryFormatter serializer = GetSerializer();
               
               Dictionary<string, object> state = (Dictionary<string, object>)serializer.Deserialize(fileStream);
               
               return state;
        }

       

        /*
        try
        {
            var save = 
            fileStream.Close();
            return save;
        }
        catch 
        {
            Debug.LogErrorFormat("Load Failed at {0}",path);
            fileStream.Close();
            return null;
        }*/
    }

    public BinaryFormatter GetSerializer()
    {
        BinaryFormatter serializer = new BinaryFormatter();

        SurrogateSelector selector = new SurrogateSelector();
        Vector3SerializationSurrogate vector3SerializationSurrogate = new Vector3SerializationSurrogate();
        QuaternionSerializationSurrogate quaternionSerializationSurrogate = new QuaternionSerializationSurrogate();
        
        selector.AddSurrogate(typeof(Vector3), new StreamingContext(StreamingContextStates.All),vector3SerializationSurrogate);
        selector.AddSurrogate(typeof(Quaternion), new StreamingContext(StreamingContextStates.All),quaternionSerializationSurrogate);

        serializer.SurrogateSelector = selector;
        return serializer;
    }
    
    public void CaptureState(Dictionary<string,object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.Id] = saveable.CaptureState();
        }
    }
    
    public IEnumerator RestoreState(Dictionary<string,object> state)
    {
        var entities = FindObjectsOfType<SaveableEntity>();
        var entitieNumber = entities.Length;
        var count = 0;
        foreach (var saveable in entities)
        {
            progress = (float) count++ / entitieNumber;
            if (state.TryGetValue(saveable.Id, out var value))
            {
                saveable.RestoreState(value);
            }
        }

        yield return new WaitForSeconds(0.5f);
        isDone = true;

    }
}
