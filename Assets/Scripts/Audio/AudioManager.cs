using UnityEngine.Audio;
using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    public AudioMixer audioMixer;


    void Awake()
    {
        // Ensures there is only ever 1 auido manager in a scene at a time
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

            DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
          s.source = gameObject.AddComponent<AudioSource>();
          s.source.clip = s.clip;

          s.source.volume = s.volume;
          s.source.pitch = s.pitch;
          s.source.loop = s.loop;

          s.source.outputAudioMixerGroup = s.outputMixerGroup;
        }

    }
    
    public void Play (String name) // Plays sound when called
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            UnityEngine.Debug.LogWarning("Sound:" + name + "not found");
            return;
        }

        s.source.Play();
    }

    public void Stop (String name) // Stops sound when called
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            UnityEngine.Debug.LogWarning("Sound:" + name + "not found");
            return;
        }

        s.source.Stop();
    }

    public void SetVolume(float volume) // Changes volume of sound
    {
        audioMixer.SetFloat("Volume", volume);
    }

}


// Put this in parts of code when sound is to play after setting up the sound in sound manager prefab
 
// FindObjectOfType<AudioManager>().Play("SoundName"); PLAYS SOUND
// OR
// FindObjectOfType<AudioManager>().Stop("SoundName"); STOPS SOUND

// Dont forget to exspose each group in the Unity Audio Mixer





