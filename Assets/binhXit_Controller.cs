using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class binhXit_Controller : MonoBehaviour
{
    [SerializeField] UnityEngine.ParticleSystem.MainModule main;
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] GameObject dan;
    [SerializeField] float timeDuration;

    [SerializeField] bool isSpawn;
    [SerializeField] float bulletForce=5f;
     float timeCoolDown;
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
            GameObject tmp = Instantiate(dan, transform.position, transform.rotation);
            timeCoolDown = timeDuration;
            Destroy(tmp, 5f);
            Rigidbody rb = tmp.GetComponent<Rigidbody>();
            rb.AddRelativeForce(Vector3.forward * bulletForce * Time.deltaTime);
        }
    }

    public void TurnOnOffFauCet()
    {




        if (main.loop)
        {

            main.loop = false;
            isSpawn = false;

        }
        else if (!main.loop)
        {

            main.loop = true;
            isSpawn = true;
        }
        _particleSystem.Play();

    }

}
