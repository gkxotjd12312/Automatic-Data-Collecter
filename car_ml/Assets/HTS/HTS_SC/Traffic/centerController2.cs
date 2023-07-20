using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerController2 : MonoBehaviour
{
    public int ret_c;
    public int random_c;

    private void Start()
    {
        ret_c = 0;
        random_c = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("car"))
        {
            ret_c = 1;

            random_c = 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("car"))
        {
            ret_c = 0;
            random_c = 0;
        }
    }
}
