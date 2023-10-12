using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic_Light_Auto1 : MonoBehaviour
{
    Renderer lightColor;

    centerFController centerF;
    centerBController centerB;
    rightController right;
    leftController left;

    [SerializeField] private GameObject centerFC;
    [SerializeField] private GameObject centerBC;
    [SerializeField] private GameObject rightC;
    [SerializeField] private GameObject leftC;

    [SerializeField] private GameObject wall_CF;
    [SerializeField] private GameObject wall_CB;
    [SerializeField] private GameObject wall_R;
    [SerializeField] private GameObject wall_L;

    private Collider wall_Rc;
    private Collider wall_CFc;
    private Collider wall_CBc;
    private Collider wall_Lc;

    public int randomNumber = 0;

    [SerializeField] private float temp_time = 0.0f;

    [SerializeField] private float max_time = 7.0f;

    private bool isBlockOn = false;

    void Start()
    {
        right = GetComponentInChildren<rightController>();
        centerF = GetComponentInChildren<centerFController>();
        centerB = GetComponentInChildren<centerBController>();
        left = GetComponentInChildren<leftController>();

        lightColor = gameObject.GetComponent<Renderer>();
        lightColor.material.color = Color.red;

        wall_Rc = wall_R.GetComponent<Collider>();
        wall_CFc = wall_CF.GetComponent<Collider>();
        wall_CBc = wall_CB.GetComponent<Collider>();
        wall_Lc = wall_L.GetComponent<Collider>();
    }

    private void Update()
    {
        if (right.ret_r == 1)
        {
            if (!isBlockOn)
            {
                wall_CFc.enabled = true;
                wall_CBc.enabled = true;

                isBlockOn = true;
            }

            temp_time += Time.deltaTime;
            RightCollider();
        }
        else if (centerF.ret_cf == 1)
        {

            if (!isBlockOn)
            {
                wall_Lc.enabled = true;
                wall_Rc.enabled = true;

                isBlockOn = true;
            }
            
            temp_time += Time.deltaTime;
            CenterFCollider();
        }
        else if (centerB.ret_cb == 1)
        {
            if (!isBlockOn)
            {
                wall_Lc.enabled = true;
                wall_Rc.enabled = true;

                isBlockOn = true;
            }

            temp_time += Time.deltaTime;
            CenterBCollider();
        }
        else if (left.ret_l == 1)
        {
            if (!isBlockOn)
            {
                wall_CFc.enabled = true;
                wall_CBc.enabled = true;

                isBlockOn = true;
            }
            temp_time += Time.deltaTime;
            LeftCollider();
        }
        else
        {
            lightColor.material.color = Color.red;
            temp_time = 0;
            randomNumber = 0;

            wall_Rc.enabled = false;
            wall_CFc.enabled = false;
            wall_CBc.enabled = false;
            wall_Lc.enabled = false;

            isBlockOn = false;
        }
    }

    private void RightCollider()
    {

        if (temp_time >= max_time)
        {
            if (isBlockOn)
            {
                wall_CFc.enabled = false;
                wall_CBc.enabled = false;
            }

            randomNumber = right.random_r;

            temp_time = 0;

            if (randomNumber == 1)
            {
                lightColor.material.color = Color.green;

                wall_CFc.enabled = true;
                wall_CBc.enabled = true;
            }
            else if (randomNumber == 2)
            {
                lightColor.material.color = Color.blue;

                wall_CFc.enabled = true;
                wall_Lc.enabled = true;
            }
            else if (randomNumber == 3)
            {
                lightColor.material.color = new Color(255 / 255f, 127 / 255f, 0 / 255f);

                wall_CBc.enabled = true;
                wall_Lc.enabled = true;
            }
        }
    }

    private void CenterFCollider()
    {
        if (temp_time >= max_time)
        {
            if (isBlockOn)
            {
                wall_Lc.enabled = false;
                wall_Rc.enabled = false;
            }

            randomNumber = centerF.random_cf;

            temp_time = 0;


            if (randomNumber == 1)
            {
                lightColor.material.color = Color.green;

                wall_Lc.enabled = true;
                wall_Rc.enabled = true;
            }
            else if (randomNumber == 2)
            {
                lightColor.material.color = Color.blue;

                wall_Lc.enabled = true;
                wall_CBc.enabled = true;
                
            }
            else if (randomNumber == 3)
            {
                lightColor.material.color = new Color(255 / 255f, 127 / 255f, 0 / 255f);

                wall_CBc.enabled = true;
                wall_Rc.enabled = true;
            }

           
        }
    }
    private void CenterBCollider()
    {
        if (temp_time >= max_time)
        {
            
            if (isBlockOn)
            {
                wall_Lc.enabled = false;
                wall_Rc.enabled = false;
            }
            randomNumber = centerB.random_cb;

            temp_time = 0;

            if (randomNumber == 1)
            {
                lightColor.material.color = Color.green;

                wall_Lc.enabled = true;
                wall_Rc.enabled = true;
            }
            else if (randomNumber == 2)
            {
                lightColor.material.color = Color.blue;

                wall_CFc.enabled = true;
                wall_Rc.enabled = true;
            }
            else if (randomNumber == 3)
            {
                lightColor.material.color = new Color(255 / 255f, 127 / 255f, 0 / 255f);

                wall_CFc.enabled = true;
                wall_Lc.enabled = true;
            }
        }
    }

    private void LeftCollider()
    {
        if (temp_time >= max_time)
        {
            if (isBlockOn)
            {
                wall_CFc.enabled = false;
                wall_CBc.enabled = false;
            }

            randomNumber = left.random_l;

            temp_time = 0;
            
            if (randomNumber == 1)
            {
                lightColor.material.color = Color.green;

                wall_CFc.enabled = true;
                wall_CBc.enabled = true;
            }
            else if (randomNumber == 2)
            {
                lightColor.material.color = Color.blue;

                wall_CBc.enabled = true;
                wall_Rc.enabled = true;
            }
            else if (randomNumber == 3)
            {
                lightColor.material.color = new Color(255 / 255f, 127 / 255f, 0 / 255f);

                wall_CFc.enabled = true;
                wall_Rc.enabled = true;
            }
        }
    }
}
