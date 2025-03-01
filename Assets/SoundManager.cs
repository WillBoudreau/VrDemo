using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Sound Manager")]  
    [SerializeField] private AudioSource audioSource; // The audio source
    [Header("Music Settings")]
    [SerializeField] private AudioClip[] audioClips; // The audio clips\
    [Header("Sound Settings")]
    [SerializeField] private AudioClip[] soundClips; // The sound clips

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayLevelTheme();
    }
    /// <summary>
    /// Play the level theme
    /// </summary>
    public void PlayLevelTheme()
    {
        Debug.Log("Playing level theme");
        audioSource.clip = audioClips[0];
        Debug.Log(audioSource.clip);
        audioSource.Play();
    }
    /// <summary>
    /// Stop all the music
    /// </summary>
    public void StopMusic()
    {
        audioSource.Stop();
    }
    /// <summary>
    /// Play the sound
    /// </summary>
    public void PlaySound(int sound)
    {
        audioSource.PlayOneShot(soundClips[sound]);
    }
}
