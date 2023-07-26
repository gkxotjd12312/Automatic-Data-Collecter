using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    [SerializeField]
    private Traffic_Light_Auto[] light_list = new Traffic_Light_Auto[3];
    private int[] light_signal_list = new int[3];
    public int light_signal = 0;


    private void Update()
    {
        for (int i=0; i<(light_list.Length);  i++)
        {
            light_signal_list[i] = light_list[i].randomNumber;
        }

        int max = light_signal_list[0];
        for (int i = 1; i < light_signal_list.Length; i++)
        {
            if (light_signal_list[i] > max)
            {
                max = light_signal_list[i];
            }
        }
        light_signal = max;
    }

}
