using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightController : MonoBehaviour
{
    public int ret_r;
    public int random_r;
    [SerializeField] private bool isTCrossing = false;

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

            if (!isTCrossing)
            {
                int randomN = Random.Range(0, 3);

                if (randomN == 0)
                {
                    random_r = 1;
                }
                else if (randomN == 1)
                {
                    random_r = 2;
                }
                else if (randomN == 2)
                {
                    random_r = 3;
                }
            }
            else
            {
                int randomN = Random.Range(0, 2);

                if (randomN == 0)
                {
                    random_r = 1;
                }
                else if (randomN == 1)
                {
                    random_r = 2;
                }
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
