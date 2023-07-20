using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerController : MonoBehaviour
{
    public int ret_c;
    public int random_c;
    [SerializeField] private bool isCorner = false;

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
            if (!isCorner)
            {
                float randomN = Random.Range(0f, 1f);

                if (randomN < 0.5f)
                {
                    random_c = 2;
                }
                else
                {
                    random_c = 3;
                }
            }
            else
            {
                random_c = 2;
            }
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
