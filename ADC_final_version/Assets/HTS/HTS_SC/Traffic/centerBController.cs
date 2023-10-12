using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerBController : MonoBehaviour
{
    public int ret_cb;
    public int random_cb;
    [SerializeField] private bool isCorner = false;
    [SerializeField] private bool isTCrossing = false;

    private void Start()
    {
        ret_cb = 0;
        random_cb = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("car"))
        {
            ret_cb = 1;

            if (!isCorner)
            {
                if (!isTCrossing)
                {
                    int randomN = Random.Range(0, 3);

                    if (randomN == 0)
                    {
                        random_cb = 1;
                    }
                    else if (randomN == 1)
                    {
                        random_cb = 2;
                    }
                    else if (randomN == 2)
                    {
                        random_cb = 3;
                    }
                }
                else 
                {
                    int randomN = Random.Range(0, 2);

                    if (randomN == 0)
                    {
                        random_cb = 2;
                    }
                    else if (randomN == 1)
                    {
                        random_cb = 3;
                    }

                }

            }
            else
            {
                random_cb = 2;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("car"))
        {
            ret_cb = 0;
            random_cb = 0;
        }
    }
}
