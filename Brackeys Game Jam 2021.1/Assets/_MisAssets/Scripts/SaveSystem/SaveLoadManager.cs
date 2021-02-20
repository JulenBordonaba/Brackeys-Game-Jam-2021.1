using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    [Header("Save Paths")]
    public string settingsPath = "Settings.json";
    public string scorePath = "Score.json";

    public event Action OnDataLoaded;

    [HideInInspector]
    public bool dataLoaded = false;


    protected override void Awake()
    {
        base.Awake();
        if (returnAwake) return;
        Application.targetFrameRate = 30;
        LoadData();
    }

    private void OnApplicationPause(bool pause)
    {
        SaveData();
    }


    private void OnApplicationQuit()
    {
        SaveData();

    }


    public void SaveData()
    {
        if (!dataLoaded) return;
        print("Save Data");
        ConfigurationManager.settings.SaveDataPlayerPrefs(settingsPath);
        //ScoreManager.record.SaveDataPlayerPrefs(scorePath);
    }

    public void LoadData()
    {

        print("Entra a load data");
        //Load configuration settings
        ConfigurationSettings settings = settingsPath.LoadDataPlayerPrefs<ConfigurationSettings>();
        ConfigurationManager.settings = SaveLoadDataExtensions.CheckData(settings);

        //Load players record
        //ScoreData record = scorePath.LoadDataPlayerPrefs<ScoreData>();
        //ScoreManager.record = SaveLoadDataExtensions.CheckData(record); 

        //data is loaded
        dataLoaded = true;
        OnDataLoaded?.Invoke();
    }
    
}
