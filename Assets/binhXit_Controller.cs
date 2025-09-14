using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class binhXit_Controller : MonoBehaviour
{
    UnityEngine.ParticleSystem.MainModule main;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] GameObject dan;
    [SerializeField] float timeDuration;

    [SerializeField] bool isSpawn;
    [SerializeField] float bulletForce=5f;
     float timeCoolDown;
     [SerializeField] Transform attach; 
     [SerializeField]  AudioSource audioSource;
     
    void Start()
    {

        main = _particleSystem.main;
        main.loop = false;

    }

    void Update()
    {
        timeCoolDown -= Time.deltaTime;
        if (isSpawn && timeCoolDown < 0)
        {
            GameObject tmp = Instantiate(dan, attach.position,attach.rotation);
            timeCoolDown = timeDuration;
            Destroy(tmp, 5f);
            Rigidbody rb = tmp.GetComponent<Rigidbody>();
            
            rb.AddRelativeForce(Vector3.forward * -bulletForce, ForceMode.Impulse);

        }
    }

    public void TurnOnOffFauCet()
    {




        if (main.loop)
        {

            main.loop = false;
            isSpawn = false;
            audioSource.Stop();

        }
        else if (!main.loop)
        {
            if (!audioSource.isPlaying)
            {

                audioSource.Play();
            }
            main.loop = true;
            isSpawn = true;
        }
        _particleSystem.Play();

    }

}
