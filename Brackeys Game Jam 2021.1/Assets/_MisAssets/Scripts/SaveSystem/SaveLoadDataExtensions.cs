using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveLoadDataExtensions
{
    #region SaveLoadData



    public static void SaveData<T>(this T _data, string path)
    {
        string completePath = Path.Combine(Application.persistentDataPath, path);
        string jsonData = JsonUtility.ToJson(_data, true);
        

        File.WriteAllText(completePath, jsonData);
    }
    
    /// <summary>
    /// This function checks if the data is null and sets a default value for it
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data">The data to check</param>
    /// <returns>Returns the data with a non null value</returns>
    public static T CheckData<T>(this T data) where T : new()
    {
        if(data==null)
        {
            data = new T();
        }

        return data;
    }


    public static T LoadData<T>(this string path)
    {
        string completePath = Path.Combine(Application.persistentDataPath, path);

        if(!File.Exists(completePath))
        {
            return default(T);
        }

        string jsonData = File.ReadAllText(completePath);

        T data = JsonUtility.FromJson<T>(jsonData);

        return data;

    }

    public static void SaveDataPlayerPrefs<T>(this T _data, string dataKey)
    {
        string jsonData = JsonUtility.ToJson(_data, true);

        PlayerPrefs.SetString(dataKey, jsonData);
        
    }

    public static T LoadDataPlayerPrefs<T>(this string dataKey)
    {
        if (!PlayerPrefs.HasKey(dataKey)) return default(T);

        string _jsonData = PlayerPrefs.GetString(dataKey);
        T _data = JsonUtility.FromJson<T>(_jsonData);
        return _data;

    }


    #endregion
}
