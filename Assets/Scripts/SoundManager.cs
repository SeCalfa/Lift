using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource audio;

    public void AudioMute(bool isAudioMute)
    {
        audio.mute = isAudioMute;
    }

}
