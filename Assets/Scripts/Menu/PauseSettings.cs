using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSettings : MonoBehaviour
{
    public Text username;
    public InputField inputUsername;
    public Slider sliderMusicVolume;
    public Slider sliderSFXVolume;
    public AudioSource bgMusic;
    // Start is called before the first frame update
    void Start()
    {
        LoadValues();
    }
    // Update is called once per frame
    public void Create()
    {
        username.text = inputUsername.text;
        PlayerPrefs.SetString("username", username.text);
        PlayerPrefs.Save();
    }

    public void SaveMusicVolume() {
        float musicVolume = sliderMusicVolume.value;
        float sfxVolume = sliderSFXVolume.value;
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();
        LoadValues();
    }

    public void LoadValues() {
        username.text = PlayerPrefs.GetString("username");
        float musicVolume = PlayerPrefs.GetFloat("musicVolume");
        float sfxVolume = PlayerPrefs.GetFloat("sfxVolume");;
        sliderMusicVolume.value = musicVolume;
        sliderSFXVolume.value = sfxVolume;
        bgMusic.volume = musicVolume;
    }
}
