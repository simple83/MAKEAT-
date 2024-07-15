using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSoundManager : MonoBehaviour
{
    public AudioClip[] buttonSounds;
    public AudioSource buttonAudioSource;

    public void PickUpSound(AudioSource buttonAudioSource)
    {
        buttonAudioSource.clip = buttonSounds[0];
        buttonAudioSource.Play();
    }

    public void ThrowSound(AudioSource buttonAudioSource)
    {
        buttonAudioSource.clip = buttonSounds[1];//아직 할당안됨
        buttonAudioSource.Play();
    }
    public void MakeFoodSound(AudioSource buttonAudioSource)
    {
        buttonAudioSource.clip = buttonSounds[2];
        buttonAudioSource.Play();
    }

    public void EatFoodSound(AudioSource buttonAudioSource)
    {
        buttonAudioSource.clip = buttonSounds[3];
        buttonAudioSource.Play();
    }
}
