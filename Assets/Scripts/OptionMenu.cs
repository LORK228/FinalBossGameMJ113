using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour
{
    public AudioMixer AudioMixer;

    public void SetVolume(float volume)
    {
        AudioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
    }
}
