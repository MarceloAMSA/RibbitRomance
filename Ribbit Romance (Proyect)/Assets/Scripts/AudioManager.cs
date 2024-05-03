using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
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
        }

    }

    // Update is called once per frame
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, item => item.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void Unmute(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
            return;
        s.source.volume = .5f;
    }

    public void Mute(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
            return;
        s.source.volume = 0f;

    }
}

