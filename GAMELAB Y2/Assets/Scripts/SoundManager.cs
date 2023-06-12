using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SoundManager: MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    private void Start()
    { 
        //if there is no data from previous session volume sets to 1 aka 100%
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    //volume equals the volume of the slider  and not changing it every time in game
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }
    //player prefs store preferences between sessions
    private void Load()
    {
        //retrieving data because it was saved as float
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        // the volume of the slider is the same that is stored in the musicVolume
    }
    private void Save()
    {
        //stores the value of the volume slider into the musicVollume key name
        //im using float because the volume slider value is of type float
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);



    }
}
