using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireController : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    [SerializeField] ParticleSystem ps2;
    [SerializeField] float dame = 0.1f;

    UnityEngine.ParticleSystem.MainModule main;
    UnityEngine.ParticleSystem.MainModule main2;

    void Start()
    {
        main = ps.main;
        main2 = ps2.main;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("dapLua"))
        {
            if (main.startSize.constantMax > 0)
            {
                Debug.Log("dapLua");

                var size = main.startSize;
                size.constantMax -= dame;
                main.startSize = size;   
            }
            // else if (main2.startSize.constantMax > 0)
            // {
            //     var size2 = main2.startSize;
            //     size2.constantMax -= dame;
            //     main2.startSize = size2; 
            // }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
