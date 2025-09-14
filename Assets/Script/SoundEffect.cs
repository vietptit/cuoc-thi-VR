using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public static SoundEffect Instance;
    public AudioSource audioSource;
    private void Start()
    {
        Instance = this;
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlaySound(audioSource.clip);
        }
    }
}
