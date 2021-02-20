using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using ExtraUI;
using UnityEngine.UI;

public class ConfigurationManager : MonoBehaviour
{
    public static ConfigurationSettings settings;

    [Header("Graphics")]
    public Slider brightnessSlider;
    public Slider vSyncSlider;
    public Slider antialiasingSlider;
    public Slider textureQualitySlider;
    public Slider shadowResolutionSlider;

    [Header("Controller")]
    public Slider sensitivitySlider;
    public Slider invertControlsSlider;
    public Slider controllerSlider;

    [Header("Music")]
    public Slider generalVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider effectVolumeSlider;

    public Dictionary<int, bool> intToBool = new Dictionary<int, bool>()
    {
        {0,false },
        {1,true }
    };

    public Dictionary<bool, int> boolToInt = new Dictionary<bool, int>()
    {
        {false,0 },
        {true,1 }
    };

    private static bool slidersSet = false;


    IEnumerator WaitUntilDataIsLoaded()
    {
        yield return new WaitUntil(() => SaveLoadManager.Instance.dataLoaded);
        SetQualitySettings();
        SetSlidervalues();

    }
    
    public void OnEnable()
    {
        StartCoroutine(WaitUntilDataIsLoaded());
    }

    private void OnDisable()
    {
        slidersSet = false;
    }

    public void SetQualitySettings()
    {
        RenderSettings.ambientLight = new Color(settings.brightness, settings.brightness, settings.brightness, 1f);
        QualitySettings.vSyncCount = (int)settings.vSync;
        QualitySettings.antiAliasing = (int)settings.antialiasing * 2;
        QualitySettings.masterTextureLimit = Mathf.Abs((int)settings.textureQuality - 3);
        QualitySettings.shadowResolution = (UnityEngine.ShadowResolution)settings.shadowResolution;
    }


    private void Update()
    {
        if (!slidersSet) return;
        //ChangeSensitivity();
        //ChangeGeneralVolume();
        //ChangeEffectVolume();
        //ChangeMusicVolume();
    }

    public void SetSlidervalues()
    {
        

        //sonido
        if (musicVolumeSlider != null)
            musicVolumeSlider.value = settings.musicVolume;
        if (effectVolumeSlider != null)
            effectVolumeSlider.value = settings.effectVolume;
        if (generalVolumeSlider != null)
            generalVolumeSlider.value = settings.generalVolume;

        //graphics
        if (vSyncSlider != null)
            vSyncSlider.value = settings.vSync;
        if (brightnessSlider != null)
            brightnessSlider.value = settings.brightness;
        if (antialiasingSlider != null)
            antialiasingSlider.value = settings.antialiasing;
        if (textureQualitySlider != null)
            textureQualitySlider.value = settings.textureQuality;
        if (shadowResolutionSlider != null)
            shadowResolutionSlider.value = settings.shadowResolution;

        //controller
        if (controllerSlider != null)
            controllerSlider.value = (int)settings.controllerType;
        if (invertControlsSlider != null)
            invertControlsSlider.value = boolToInt[settings.inverted];
        if (sensitivitySlider != null)
            sensitivitySlider.value = settings.sensitivity.y;

        slidersSet = true;
    }


    #region Volume

    public void ChangeMusicVolume()
    {
        settings.musicVolume = musicVolumeSlider.value;
    }

    public void ChangeEffectVolume()
    {
        settings.effectVolume = effectVolumeSlider.value;
    }

    public void ChangeGeneralVolume()
    {
        settings.generalVolume = generalVolumeSlider.value;
    }

    #endregion

    #region Controller

    public void ChangeSensitivity()
    {
        settings.sensitivity.x = sensitivitySlider.value;
        settings.sensitivity.y = sensitivitySlider.value;
    }

    public void ChooseController()
    {
        settings.controllerType = (ControllerType)controllerSlider.value;
    }

    public void Inverted()
    {
        settings.inverted = intToBool[(int)invertControlsSlider.value];
    }

    #endregion

    #region Graphics

    public void TextureQuality()
    {
        settings.textureQuality = textureQualitySlider.value;
        SetQualitySettings();
    }

    public void ShadowResolution()
    {
        settings.shadowResolution = shadowResolutionSlider.value;
        SetQualitySettings();
    }

    public void Brightness()
    {
        settings.brightness = brightnessSlider.value;
        SetQualitySettings();
    }

    public void VSync()
    {
        settings.vSync = (int)vSyncSlider.value;
        SetQualitySettings();
    }

    public void AntiAliasing(bool active)
    {
        settings.antialiasing = (int)antialiasingSlider.value;
        SetQualitySettings();
    }

    #endregion

}

[System.Serializable]
public class ConfigurationSettings
{
    public float brightness = RenderSettings.ambientLight.r;
    public float vSync = QualitySettings.vSyncCount;
    public float antialiasing = QualitySettings.antiAliasing / 2f;
    public float textureQuality = Mathf.Abs(QualitySettings.masterTextureLimit - 3);
    public float shadowResolution = (int)QualitySettings.shadowResolution;

    public bool inverted = false;
    public float generalVolume = 1;
    public float musicVolume = 1;
    public float effectVolume = 1;
    public Vector2 sensitivity = Vector2.one;
    public ControllerType controllerType = ControllerType.PS4;
    public string lenguage = "";
}
