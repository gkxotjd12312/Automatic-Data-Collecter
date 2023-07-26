using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftController : MonoBehaviour
{
    public int ret_l;
    public int random_l;
    [SerializeField] private bool isCorner = false;

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
                float randomN = Random.Range(0f, 1f);

                if (randomN < 0.5f)
                {
                    random_l = 1;
                }
                else
                {
                    random_l = 3;
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
