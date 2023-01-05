using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioWalkPlayer : MonoBehaviour
{
    public AudioSource sources;
    public List<AudioClip> clip;

    public void WalkAudio()
    {
        sources.clip = clip[Random.Range(0, clip.Count)];
        sources.Play();
    }
}
