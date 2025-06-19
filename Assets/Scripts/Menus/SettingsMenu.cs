using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMusicVolume (float volume) // Controls volume of music
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetEFXsVolume(float volume) // Controls volume of SFXs
    {
        audioMixer.SetFloat("SFXs", volume);
    }

    public void onHandleClick() // Plays Sound effect when clicked
    {
        FindObjectOfType<AudioManager>().Play("Jump");
    }
}


