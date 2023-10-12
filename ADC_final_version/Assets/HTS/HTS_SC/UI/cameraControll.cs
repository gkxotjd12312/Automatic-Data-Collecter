using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControll : MonoBehaviour
{
    public Transform targetobject;
    public Vector3 offset;

    private void Update()
    {
        //transform.position = targetobject.position + new Vector3(0,3.2f,2f);
        transform.position = targetobject.position + new Vector3(0, 5f, 0);
        Quaternion targetRotation = Quaternion.LookRotation(targetobject.forward, targetobject.up);
        targetRotation *= Quaternion.Euler(90, 0, 90);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation,1);
    }

}
