using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faucet_controller : MonoBehaviour
{
    
    [SerializeField] Transform _position;

    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] UnityEngine.ParticleSystem.MainModule main;
    [SerializeField] GameObject prefab;
    [SerializeField] float timeCoolDown;
    [SerializeField] Transform attach;
    [SerializeField] float bulletForce;
    AudioSource audioSource;
    // [SerializeField] private AudioClip music;

    float timer;
    bool isSpawn;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _particleSystem.gameObject.transform.position = _position.position;
        _particleSystem.gameObject.transform.rotation = _position.rotation;
        main = _particleSystem.main;
        main.loop = false;
    }

    void Update()
    {   
        timer-=Time.deltaTime;

        if (timer <= 0 && isSpawn)
        {
            timer = timeCoolDown;
            GameObject tmp = Instantiate(prefab, attach.position, attach.rotation);
            Destroy(tmp, 5f);
            Rigidbody rb = tmp.GetComponent<Rigidbody>();
            rb.AddRelativeForce(Vector3.forward * -bulletForce, ForceMode.Impulse);

        }
    }

    public void TurnOnOffFauCet()
    {


        Debug.Log("da vao");
        _particleSystem.gameObject.transform.position = _position.position;
        _particleSystem.gameObject.transform.rotation = _position.rotation;
        if (main.loop)
        {
            isSpawn = false;
            main.loop = false;
            audioSource.Stop();


        }
        else if (!main.loop)
        {
            isSpawn = true;
            main.loop = true;
            audioSource.Play();
        }
        _particleSystem.Play();

    }
}
