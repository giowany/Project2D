using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GunAudioBase : MonoBehaviour
{
    public List<AudioSource> audioSources;
    public AudioSource audioReload;

    private int _checkList = 0;

    public void PlayAudio()
    {
        if(audioSources != null)
        {
            if(_checkList < audioSources.Count)
            {
                audioSources[_checkList].Play();
                _checkList++;
                if(_checkList >= audioSources.Count)
                    _checkList = 0;
            }
        }
    }

    public void PlayAudioReload()
    {
        audioReload.Play();
    }

}
