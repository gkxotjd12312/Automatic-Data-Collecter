using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic_Light_Auto : MonoBehaviour
{
    Renderer lightColor;

    rightController right;
    centerController center;
    leftController left;

    [SerializeField] private GameObject rightC;
    [SerializeField] private GameObject centerC;
    [SerializeField] private GameObject leftC;

    [SerializeField] private GameObject wall_R;
    [SerializeField] private GameObject wall_C;
    [SerializeField] private GameObject wall_L;

    private Collider wall_Rc;
    private Collider wall_Cc;
    private Collider wall_Lc;

    public int randomNumber = 0;

    private float temp_time = 0.0f;

    private float max_time = 7.0f;
    void Start()
    {
        right = GetComponentInChildren<rightController>();
        center = GetComponentInChildren<centerController>();
        left = GetComponentInChildren<leftController>();

        lightColor = gameObject.GetComponent<Renderer>();
        lightColor.material.color = Color.red;

        wall_Rc = wall_R.GetComponent<Collider>();
        wall_Cc = wall_C.GetComponent<Collider>();
        wall_Lc = wall_L.GetComponent<Collider>();

    }

    private void Update()
    {
        if (right.ret_r == 1)
        {
            temp_time += Time.deltaTime;
            RightCollider();
        }
        else if (center.ret_c == 1)
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
            temp_time = 0;
            randomNumber = 0;

            wall_Rc.enabled = false;
            wall_Cc.enabled = false;
            wall_Lc.enabled = false;
        }
    }

    private void RightCollider()
    {

        if (temp_time >= max_time)
        {

            randomNumber = right.random_r;
            
            temp_time = 0;

            if (randomNumber == 1)
            {
                lightColor.material.color = Color.green;

                wall_Cc.enabled = true;
            }
            else if (randomNumber == 2)
            {
                lightColor.material.color = Color.blue;

                wall_Lc.enabled = true;
            }
        }
    }

    private void CenterCollider()
    {
        if (temp_time >= max_time)
        {
            randomNumber = center.random_c;

            temp_time = 0;
            if (randomNumber == 2)
            {
                lightColor.material.color = Color.blue;

                wall_Rc.enabled = true;
            }
            else if (randomNumber == 3)
            {
                lightColor.material.color = new Color(255 / 255f, 127 / 255f, 0 / 255f);

                wall_Lc.enabled = true;
            }
        }
    }

    private void LeftCollider()
    {
        if (temp_time >= max_time)
        {
            randomNumber = left.random_l;

            temp_time = 0;
            
            if (randomNumber == 1)
            {
                lightColor.material.color = Color.green;

                wall_Cc.enabled = true;
            }
            else if (randomNumber == 3)
            {
                lightColor.material.color = new Color(255 / 255f, 127 / 255f, 0 / 255f);

                wall_Rc.enabled = true;
            }
        }
    }
}
