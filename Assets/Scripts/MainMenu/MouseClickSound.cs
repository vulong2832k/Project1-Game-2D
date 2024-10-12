using System;
using UnityEngine;

public class MouseClickSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clickSound;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayClickSound();
        }
    }

    private void PlayClickSound()
    {
        if(_audioSource == null &&  _clickSound == null) return;

        if (_audioSource != null && _clickSound != null)
        {
            _audioSource.PlayOneShot(_clickSound);
        }
    }
}
