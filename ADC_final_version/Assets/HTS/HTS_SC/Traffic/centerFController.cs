using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerFController : MonoBehaviour
{
    public int ret_cf;
    public int random_cf;

    private void Start()
    {
        ret_cf = 0;
        random_cf = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("car"))
        {
            ret_cf = 1;

            int randomN = Random.Range(0, 3);

            if (randomN == 0)
            {
                random_cf = 1;
            }
            else if (randomN == 1)
            {
                random_cf = 2;
            }
            else if (randomN == 2)
            {
                random_cf = 3;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("car"))
        {
            ret_cf = 0;
            random_cf = 0;
        }
    }
}
