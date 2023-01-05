using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioJumpLand : MonoBehaviour
{
    public List<AudioClip> audioClipsLand;
    public List<AudioClip> audioClipsJump;
    public AudioSource audioSource;

    public void LandAudio()
    {
        audioSource.clip = audioClipsLand[Random.Range(0, audioClipsLand.Count)];
        audioSource.Play();
    }

    public void JumpAudio()
    {
        audioSource.clip = audioClipsJump[Random.Range(0, audioClipsJump.Count)];
        audioSource.Play();
    }
}
