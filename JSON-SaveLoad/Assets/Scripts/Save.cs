using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Save : MonoBehaviour
{
    //make singleton
    public static Save instance = null;

    //game objects to save
    [SerializeField] private List<GameObject> saveableObjects = new List<GameObject>();

    //reference to saveableOjectsData script
    public SaveableObjectsData saveableObjectsData;

    //name of file to save to
    private string file = "SaveData.txt";

    //There should be only one Save class in the scene
    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void SaveObjects()
    {
        // starting counter for ID
        int counter = 0;

        //assigning save data from each game object
        foreach (GameObject saveableObject in saveableObjects)
        {
            //create new save data
            SaveData saveData = new SaveData();

            //assign values
            saveData.ID = counter++;
            saveData.name = saveableObject.name;
            saveData.position = saveableObject.transform.position;

            //add saveable objects to saveableObjectsData
            saveableObjectsData.saveableObjects.Add(saveData);
        }

        //saving save data as json to file
        string json = JsonUtility.ToJson(saveableObjectsData);
        WriteToFile(file, json);
    }

    //writing the file
    private void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    //setting the path for the file to be saved
    private string GetFilePath(string fileName)
    {
        return Environment.ExpandEnvironmentVariables("%USERPROFILE%\\") + fileName;
    }
}
