using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightController : MonoBehaviour
{
    public int ret_r;
    public int random_r;

    private void Start()
    {
        ret_r = 0;
        random_r = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("car"))
        {
            ret_r = 1;

            float randomN = Random.Range(0f, 1f);

            if (randomN < 0.5f)
            {
                random_r = 1;
            }
            else
            {
                random_r = 2;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("car"))
        {
            ret_r = 0;
            random_r = 0;
        }
    }
}
