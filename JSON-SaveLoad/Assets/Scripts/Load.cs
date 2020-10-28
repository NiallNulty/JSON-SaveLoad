using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Load : MonoBehaviour
{
    //list of save data
    private SaveableObjectsData saveableObjectsData = new SaveableObjectsData();

    //prefab for saveable game object
    [SerializeField] private GameObject saveableGameObject;

    //loads the file
    public void LoadData()
    {
        string asset = ReadFromFile("SaveData.txt");
 
        saveableObjectsData = JsonUtility.FromJson<SaveableObjectsData>(asset);

        InstantiateSavedObjects();
    }

    //instantiates objects at saved position
    private void InstantiateSavedObjects()
    {
        for (int i = 0; i < saveableObjectsData.saveableObjects.Count; i++)
        {
            GameObject loadedGameObject = Instantiate(saveableGameObject);
            loadedGameObject.transform.name = saveableObjectsData.saveableObjects[i].name;
            loadedGameObject.transform.position = saveableObjectsData.saveableObjects[i].position;
        }
    }

    private string GetFilePath(string fileName)
    {
        return Environment.ExpandEnvironmentVariables("%USERPROFILE%\\") + fileName;
    }

    private string ReadFromFile(string fileName)
    {
        string path = GetFilePath(fileName);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }

        }
        else
            Debug.LogWarning("File not found");

        return "";
    }
}
