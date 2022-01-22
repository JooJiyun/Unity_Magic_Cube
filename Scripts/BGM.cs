using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioClip[] BGMs;
    private AudioSource BGM_Audio;

    void Start()
    {
        BGM_Audio = transform.GetComponent<AudioSource>();

        int BGM_no = PlayerPrefs.GetInt("BGM", 0);
        BGM_Audio.clip = BGMs[BGM_no];
        BGM_Audio.volume = PlayerPrefs.GetFloat("BGM_v", 80) / 100;
        BGM_Audio.loop = true;
        BGM_Audio.Play();
    }

    public void ChangeMusic(int no)
    {
        BGM_Audio.clip = BGMs[no];
        BGM_Audio.volume = PlayerPrefs.GetFloat("BGM_v", 80) / 100;
        BGM_Audio.loop = true;
        BGM_Audio.Play();
    }

    public void ChangeVolume(float vol)
    {
        vol /= 100;
        if (vol == BGM_Audio.volume) return;
        BGM_Audio.volume = vol;
        BGM_Audio.Play();
    }
}
