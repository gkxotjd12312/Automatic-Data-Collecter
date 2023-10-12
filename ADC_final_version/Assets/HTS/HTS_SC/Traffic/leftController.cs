using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftController : MonoBehaviour
{
    public int ret_l;
    public int random_l;
    [SerializeField] private bool isCorner = false;
    [SerializeField] private bool isTCrossing = false;

    private void Start()
    {
        random_l = 0;
        ret_l = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("car"))
        {
            ret_l = 1;

            if (!isCorner)
            {
                if (!isTCrossing)
                {
                    int randomN = Random.Range(0, 3);

                    if (randomN == 0)
                    {
                        random_l = 1;
                    }
                    else if (randomN == 1)
                    {
                        random_l = 2;
                    }
                    else if (randomN == 2)
                    {
                        random_l = 3;
                    }
                }
                else
                {
                    int randomN = Random.Range(0, 2);

                    if (randomN == 0)
                    {
                        random_l = 1;
                    }
                    else if (randomN == 1)
                    {
                        random_l = 3;
                    }
                }
            }
            else
            {
                random_l = 3;
            }
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
