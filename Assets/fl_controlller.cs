using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fl_controlller : MonoBehaviour
{

    [SerializeField] Light _light;

    bool isLighting;


    void Start()
    {

    }

    public void TurnOnOffTheLight(bool tf)
    {
        isLighting = tf;
    }

    void Update()
    {
        if (isLighting)
            _light.intensity = 5;
        else
            _light.intensity = 0f;
    }




}
