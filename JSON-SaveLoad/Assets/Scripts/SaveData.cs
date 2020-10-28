using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int ID;
    public string name;
    public Vector3 position;
}

[Serializable]
public class SaveableObjectsData
{
    public List<SaveData> saveableObjects;
}