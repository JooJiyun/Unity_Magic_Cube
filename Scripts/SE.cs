using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour
{
    public AudioClip[] audios;
    private AudioSource SE_Audio;
    private void Start()
    {
        SE_Audio = transform.GetComponent<AudioSource>();
    }
    public void WinSound()
    {
        PlaySE(0);
    }
    public void CubeSound()
    {
        PlaySE(1);
    }
    public void ButtonSound()
    {
        PlaySE(2);
    }
    private void PlaySE(int idx)
    {
        SE_Audio.clip = audios[idx];
        SE_Audio.loop = false;
        SE_Audio.volume = PlayerPrefs.GetFloat("SE_v", 80) / 100;
        SE_Audio.Play();
    }
}
