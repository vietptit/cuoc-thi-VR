using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faucet_controller : MonoBehaviour
{
    
    [SerializeField] Transform _position;

    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] UnityEngine.ParticleSystem.MainModule main;
    void Start()
    {
        _particleSystem.gameObject.transform.position = _position.position;
        _particleSystem.gameObject.transform.rotation = _position.rotation;
        main = _particleSystem.main;
        main.loop = false;
    }
    public void TurnOnOffFauCet()
    {


        Debug.Log("da vao");
        _particleSystem.gameObject.transform.position = _position.position;
        _particleSystem.gameObject.transform.rotation = _position.rotation;
        if (main.loop)
        {

            main.loop = false;


        }
        else if (!main.loop)
        {

            main.loop = true;
        }
        _particleSystem.Play();

    }
}
