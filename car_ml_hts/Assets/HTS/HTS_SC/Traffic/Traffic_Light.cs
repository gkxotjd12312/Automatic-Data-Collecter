using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic_Light : MonoBehaviour
{
    Renderer lightColor;

    void Start()
    {
        lightColor = gameObject.GetComponent<Renderer>();
        lightColor.material.color = Color.white;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            lightColor.material.color = Color.red;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            lightColor.material.color = Color.green;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            lightColor.material.color = Color.magenta;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            lightColor.material.color = Color.blue;
        }

    }
}
