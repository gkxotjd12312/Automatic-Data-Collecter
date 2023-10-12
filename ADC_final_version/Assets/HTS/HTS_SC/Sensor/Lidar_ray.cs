using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lidar_ray : MonoBehaviour
{
    [SerializeField] private GameObject markerPrefab;
    [Range(3, 15)][SerializeField] private int term_degree = 10;
    
    private GameObject[] markerInstance;
    private int angleRay = 0;
    private int angleAdd = 0;

    private void Start()
    {
        angleAdd = 360 / term_degree;
        markerInstance = new GameObject[angleAdd];

        for (int i = 0; i < angleAdd; i++)
        {
            markerInstance[i] = Instantiate(markerPrefab, transform);
            markerInstance[i].SetActive(false);
        }
    }

    private void Update()
    {
        for (int i = 0; i < angleAdd; i++)
        {
            float radianAngle = angleRay * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(radianAngle), 0, Mathf.Sin(radianAngle));

            RaycastMakeObj(direction,i);

            angleRay += term_degree;

            if(angleRay >= 360)
            {
                angleRay = 0;
            }
        }
    }

    void RaycastMakeObj(Vector3 dir,int i)
    {
        Ray ray = new Ray(transform.position, dir);
        RaycastHit hit;

        int layerMask = 1 << LayerMask.NameToLayer("wood") | 1 << LayerMask.NameToLayer("halfb") | 1 << LayerMask.NameToLayer("oneb") | 1 << LayerMask.NameToLayer("twob");

        if (Physics.Raycast(ray, out hit, 20, layerMask))
        {
            markerInstance[i].transform.position = hit.point;
            markerInstance[i].SetActive(true);
        }
        else
        {
            markerInstance[i].SetActive(false);
        }
    }
}
