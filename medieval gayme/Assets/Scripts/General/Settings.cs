using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;
using System.IO;

public class Settings : MonoBehaviour
{
    [BoxGroup("Menu")]
    public Menu settingsMenu;
    [BoxGroup("Menu")]
    public Transform settingsObject;

    [BoxGroup("Settings")]
    [BoxGroup("Settings/Saving")]
    public string savePath;
    [BoxGroup("Settings/Saving")]
    public SettingsSaveData saveData;

    [BoxGroup("Settings/Graphics")]
    public UniversalRenderPipelineAsset pipelineAsset;
    [BoxGroup("Settings/Graphics")]
    public TMP_Dropdown resolutionDropdown;
    [BoxGroup("Settings/Graphics")]
    public TMP_Dropdown shadowDropDown;
    [BoxGroup("Settings/Graphics")]
    public TMP_Dropdown antiAliasingDropDown;
    [BoxGroup("Settings/Graphics")]
    public TMP_Dropdown fullscreendropDown;
    Resolution[] resolutions;

    [BoxGroup("Settings/Audio")]
    public AudioMixer audioMixer;
    [BoxGroup("Settings/Audio")]
    public Slider masterSlider;
    [BoxGroup("Settings/Audio")]
    public TMP_Text masterText;
    [BoxGroup("Settings/Audio")]
    public Slider musicSlider;
    [BoxGroup("Settings/Audio")]
    public TMP_Text musicText;
    [BoxGroup("Settings/Audio")]
    public Slider effectsSlider;
    [BoxGroup("Settings/Audio")]
    public TMP_Text effectsText;

    void Start() 
    {        
        savePath = Application.persistentDataPath + "/Settings.json";

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + 
                    resolutions[i].height + " @ " + Mathf.RoundToInt(resolutions[i].refreshRateRatio.numerator);
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width 
                && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        //LoadSettings(currentResolutionIndex);

        if(File.Exists(savePath)){
            StartCoroutine(LoadCo());
        }
    }

    public void UpdateMaster(float vol)
    {
        audioMixer.SetFloat("MasterVol", vol);
        masterText.text = "Master: " + (vol + 80).ToString();
        saveData.audioSaveData.masterVolume = vol;
        SaveSettings();
        
    }
    public void UpdateMusic(float vol)
    {
        audioMixer.SetFloat("MusicVol", vol);
        musicText.text = "Music: " + (vol + 80).ToString();
        saveData.audioSaveData.musicVolume = vol;
        SaveSettings();
    }
    public void UpdateEffects(float vol)
    {
        audioMixer.SetFloat("EffectsVol", vol);
        effectsText.text = "Effects: " + (vol + 80).ToString();
        saveData.audioSaveData.effectsVolume = vol;
        SaveSettings();
    }

    public void SetResolution(int value)
    {
        Resolution resolution = resolutions[value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        saveData.graphicsSaveData.resolution = value;
        SaveSettings();
    }
    public void UpdateAntiAliasing(int value)
    {
        switch (value)
        {
            case 0:
                pipelineAsset.msaaSampleCount = (int)MsaaQuality.Disabled;
            break;    
            case 1:
                pipelineAsset.msaaSampleCount = (int)MsaaQuality._2x;
            break;    
            case 2:
                pipelineAsset.msaaSampleCount = (int)MsaaQuality._4x;
            break;    
            case 3:
                pipelineAsset.msaaSampleCount = (int)MsaaQuality._8x;
            break;    
        }
        saveData.graphicsSaveData.antiAliasingQuality = value;
        SaveSettings();
    }
    
    public void UpdateShadows(int value)
    {
        switch (value)
        {
            case 0:
                pipelineAsset.shadowDistance = 0;
            break;    
            case 1:
                pipelineAsset.shadowDistance = 150;
            break;    
            case 2:
                pipelineAsset.shadowDistance = 200;
            break;    
            case 3:
                pipelineAsset.shadowDistance = 250;
            break;    
        }
        saveData.graphicsSaveData.shadowQuality = value;
        SaveSettings();
    }
    public void UpdateTextures(int value)
    {
        switch (value)
        {
            case 0:
                
            break;    
            case 1:
                pipelineAsset.shadowDistance = 150;
            break;    
            case 2:
                pipelineAsset.shadowDistance = 200;
            break;    
            case 3:
                pipelineAsset.shadowDistance = 250;
            break;    
        }
        saveData.graphicsSaveData.texureQuality = value;
        SaveSettings();
    }

    public void UpdateFullscreen(int value)
    {
        switch (value)
        {
            case 0:
                Screen.fullScreenMode = FullScreenMode.Windowed;
            break;
            case 1:
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            break;
            case 2:
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            break;
        }
        saveData.graphicsSaveData.fullscreen = value;
        SaveSettings();
    }

    public void SaveSettings()
    {
        string jsonString = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, jsonString);
    }

    public IEnumerator LoadCo()
    {
        settingsObject.position = new Vector3(10000, 10000, 0);
        settingsMenu.Enable();
        LoadSettings();
        yield return new WaitForEndOfFrame();
        settingsMenu.Disable();
        settingsObject.localPosition = new Vector3(0, 0, 0);
    }

    public void LoadSettings()
    {
        string fileContents = File.ReadAllText(savePath);

        SettingsSaveData newSaveData = JsonUtility.FromJson<SettingsSaveData>(fileContents);

        fullscreendropDown.value = newSaveData.graphicsSaveData.fullscreen;
        UpdateFullscreen(fullscreendropDown.value);
        shadowDropDown.value = newSaveData.graphicsSaveData.shadowQuality;
        UpdateShadows(shadowDropDown.value);
        antiAliasingDropDown.value = newSaveData.graphicsSaveData.antiAliasingQuality;
        UpdateAntiAliasing(antiAliasingDropDown.value);
        resolutionDropdown.value = newSaveData.graphicsSaveData.resolution;
        SetResolution(resolutionDropdown.value);
        
        masterSlider.value = newSaveData.audioSaveData.masterVolume;
        UpdateMaster(masterSlider.value);
        musicSlider.value = newSaveData.audioSaveData.musicVolume;
        UpdateMusic(musicSlider.value);
        effectsSlider.value = newSaveData.audioSaveData.effectsVolume;
        UpdateEffects(effectsSlider.value);
    }
}

[System.Serializable]
public class SettingsSaveData
{
    public GraphicsSaveData graphicsSaveData;
    public AudioSaveData audioSaveData;
    public GameplaySaveData gameplaySaveData;
}
[System.Serializable]
public class GraphicsSaveData
{
    public int texureQuality;
    public int shadowQuality;
    public int antiAliasingQuality;
    public int resolution;
    public int fullscreen;
}
[System.Serializable]
public class AudioSaveData
{
    public float masterVolume;
    public float effectsVolume;
    public float musicVolume;
}
[System.Serializable]
public class GameplaySaveData
{

}

