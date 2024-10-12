using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip playerShoot;
    public AudioClip playerDead;

    private void Start()
    {
        _musicSource.clip = background;
        _musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }
    public void StopMusic()
    {
        if (_musicSource.isPlaying)
        {
            _musicSource.Stop();
        }
    }
}
