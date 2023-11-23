using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;

public class Settings : MonoBehaviour
{
    [BoxGroup("Settings")]
    [BoxGroup("Settings/Graphics")]
    public UniversalRenderPipelineAsset pipelineAsset;
    [BoxGroup("Settings/Graphics")]
    public TMP_Dropdown resolutionDropdown;
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
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + 
                    resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width 
                && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
        //LoadSettings(currentResolutionIndex);
    }

    public void UpdateMaster(float vol)
    {
        audioMixer.SetFloat("MasterVol", vol);
        masterText.text = "Master: " + (vol + 80).ToString();
    }
    public void UpdateMusic(float vol)
    {
        audioMixer.SetFloat("MusicVol", vol);
        masterText.text = "Music: " + (vol + 80).ToString();
    }
    public void UpdateEffects(float vol)
    {
        audioMixer.SetFloat("EffectsVol", vol);
        masterText.text = "Effects: " + (vol + 80).ToString();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
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
    public int resolution;
    public int windowType;
    public bool fullscreen;
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

