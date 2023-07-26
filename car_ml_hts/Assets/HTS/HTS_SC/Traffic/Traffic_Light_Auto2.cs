using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic_Light_Auto2 : MonoBehaviour
{
    Renderer lightColor;

    centerController2 center;
    leftController2 left;

    [SerializeField] private GameObject centerC;
    [SerializeField] private GameObject leftC;

    private int randomNumber = 0;

    private float temp_time = 0.0f;
    private float max_time = 10.0f;

    void Start()
    {
        center = GetComponentInChildren<centerController2>();
        left = GetComponentInChildren<leftController2>();

        lightColor = gameObject.GetComponent<Renderer>();
        lightColor.material.color = Color.red;
    }

    private void Update()
    {
        if (center.ret_c == 1)
        {
            temp_time += Time.deltaTime;
            CenterCollider();
        }
        else if (left.ret_l == 1)
        {
            temp_time += Time.deltaTime;
            LeftCollider();
        }
        else
        {
            lightColor.material.color = Color.red;

        }
    }

    private void CenterCollider()
    {
        randomNumber = center.random_c;

        if (temp_time >= max_time)
        {
            temp_time = 0;
            lightColor.material.color = Color.blue;
        }
    }

    private void LeftCollider()
    {
        randomNumber = left.random_l;

        if (temp_time >= max_time)
        {
            temp_time = 0;
            lightColor.material.color = new Color(255 / 255f, 127 / 255f, 0 / 255f);
        }
    }
}
