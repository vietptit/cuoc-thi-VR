using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicDienthoai : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;


    public void turnOnCall114()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
        else audioSource.Stop();
    }
}
