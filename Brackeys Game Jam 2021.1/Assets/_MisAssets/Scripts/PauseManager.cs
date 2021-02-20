using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : Singleton<PauseManager>
{


    public bool onPause = false;

    #region Before pause settings

    private PauseSettings pauseSettings = new PauseSettings();

    #endregion

    

    public void Pause()
    {

        pauseSettings?.SaveSettings();

        PauseSettings.PauseConfiguration.ApplySettings();

        onPause = true;
    }

    public void Resume()
    {
        pauseSettings?.ApplySettings();

        onPause = false;
    }
    

    private void OnLevelWasLoaded(int level)
    {
        Time.timeScale = 1;
    }


}

public class PauseSettings
{
    public bool cursorVisible = true;
    public CursorLockMode cursorLockMode = CursorLockMode.None;
    public float timeScale = 1;

    public PauseSettings()
    {
        cursorVisible = true;
        cursorLockMode = CursorLockMode.None;
        timeScale = 1;
    }

    public void ApplySettings()
    {
        Cursor.visible = cursorVisible;
        Cursor.lockState = cursorLockMode;
        Time.timeScale = timeScale;
    }

    public void SaveSettings()
    {
        
        cursorVisible = Cursor.visible;
        cursorLockMode = Cursor.lockState;
        timeScale = Time.timeScale;
    }

    public static PauseSettings PauseConfiguration
    {
        get
        {

            PauseSettings ps = new PauseSettings();
            ps.timeScale = 0;
            ps.cursorVisible = true;
            ps.cursorLockMode = CursorLockMode.None;
            return ps;
        }
    }
}
