using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_sc : MonoBehaviour
{
    public GameObject explain;
    public GameObject caution;
    public GameObject mainM;

    private void Start()
    {
        explain.SetActive(false);
        caution.SetActive(false);
    }

    public void first_change()
    {
        SceneManager.LoadScene("1.Image_Scene");
    }
    public void second_change()
    {
        SceneManager.LoadScene("2.Segment_Scene");
    }
    public void third_change()
    {
        SceneManager.LoadScene("3.Lidar_Scene");
    }
    public void exit_change()
    {
        Application.Quit();
    }
    public void exp_change()
    {
        explain.SetActive(true);
        mainM.SetActive(false);
    }
    public void caution_change()
    {
        caution.SetActive(true);
        mainM.SetActive(false);
    }
    public void main_change()
    {
        mainM.SetActive(true);
        
        explain.SetActive(false);
        caution.SetActive(false);
    }
}
