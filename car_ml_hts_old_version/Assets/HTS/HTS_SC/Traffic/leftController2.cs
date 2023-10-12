using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftController2 : MonoBehaviour
{
    public int ret_l;
    public int random_l;

    private void Start()
    {
        ret_l = 0;
        random_l = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("car"))
        {
            ret_l = 1;
            random_l = 3;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("car"))
        {
            ret_l = 0;
            random_l = 0;
        }
    }
}
