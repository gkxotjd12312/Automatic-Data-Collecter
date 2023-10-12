using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class shader_SC : MonoBehaviour
{
    private string folderPath;
    private string imageFilePath;
    private int filenumber = 1;
    private bool camera_on = false;
    float captureInterval = 5.0f;
    float timer = 0f;

    public random_sky_box rdsb;

    private void Awake()
    {
        folderPath = Path.Combine(Application.persistentDataPath, "segmentation_F");
        imageFilePath = Path.Combine(folderPath, "Captures");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);

            if (!Directory.Exists(imageFilePath))
            {
                Directory.CreateDirectory(imageFilePath);
            }
        }
        if (!Directory.Exists(imageFilePath))
        {
            Directory.CreateDirectory(imageFilePath);
        }
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.C))
        {
            camera_on = !camera_on;
        }

        if (camera_on && timer > captureInterval)
        {
            StartCoroutine(CaptureSequence());
            timer = 0;
        }
    }

    IEnumerator CaptureSequence()
    {
        string imageName = $"origin_{filenumber:D7}";
        string imagePath = Path.Combine(imageFilePath, $"{imageName}.png");
        ScreenCapture.CaptureScreenshot(imagePath);

        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.05f);  // ¿¹¸¦ µé¾î 0.5ÃÊÀÇ µô·¹ÀÌ¸¦ Ãß°¡

        change_shader();

        string imageName_2 = $"segmentation_{filenumber:D7}";
        string imagePath_2 = Path.Combine(imageFilePath, $"{imageName_2}.png");
        ScreenCapture.CaptureScreenshot(imagePath_2);

        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(0.05f);

        filenumber++;
        Debug.Log("segmentationÂûÄ¬");
        reboot_shader();

        rdsb.ChangeSky();
    }



    void reboot_shader()
    {
        Shader standardShader = Shader.Find("Standard");

        int woodLayer = LayerMask.NameToLayer("wood");
        int streetLayer = LayerMask.NameToLayer("street");
        int crosslineLayer = LayerMask.NameToLayer("cross_line");
        int halfLayer = LayerMask.NameToLayer("halfb");
        int oneLayer = LayerMask.NameToLayer("oneb");
        int twoLayer = LayerMask.NameToLayer("twob");
        int trafficLayer = LayerMask.NameToLayer("traffic");
        int skyLayer = LayerMask.NameToLayer("sky");


        GameObject[] allobjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allobjects)
        {
            if (obj.layer == woodLayer || obj.layer == streetLayer || obj.layer == crosslineLayer || obj.layer == halfLayer ||
                obj.layer == oneLayer || obj.layer == twoLayer || obj.layer == trafficLayer || obj.layer == skyLayer)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                foreach (Material mat in renderer.materials)
                {
                    mat.shader = standardShader;
                }
            }

        }

    }


    void change_shader()
    {
        Shader woodshader = Resources.Load<Shader>("Shader/wood");
        Shader streetshader = Resources.Load<Shader>("Shader/street");
        Shader halfshader = Resources.Load<Shader>("Shader/half");
        Shader oneshader = Resources.Load<Shader>("Shader/one");
        Shader twoshader = Resources.Load<Shader>("Shader/two");
        Shader trafficshader = Resources.Load<Shader>("Shader/traffic");
        Shader skyshader = Resources.Load<Shader>("Shader/sky");



        int woodLayer = LayerMask.NameToLayer("wood");
        int streetLayer = LayerMask.NameToLayer("street");
        int crosslineLayer = LayerMask.NameToLayer("cross_line");
        int halfLayer = LayerMask.NameToLayer("halfb");
        int oneLayer = LayerMask.NameToLayer("oneb");
        int twoLayer = LayerMask.NameToLayer("twob");
        int trafficLayer = LayerMask.NameToLayer("traffic");
        int skyLayer = LayerMask.NameToLayer("sky");


        GameObject[] allobjects = FindObjectsOfType<GameObject>();

        foreach(GameObject obj in allobjects)
        {
            if(obj.layer == woodLayer)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                foreach (Material mat in renderer.materials)
                {
                    mat.shader = woodshader;
                }
            }

            if (obj.layer == streetLayer || obj.layer == crosslineLayer)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                
                foreach (Material mat in renderer.materials)
                {
                    mat.shader = streetshader;
                }
            }
            if (obj.layer == halfLayer)
            {
                Renderer renderer = obj.GetComponent<Renderer>();

                foreach (Material mat in renderer.materials)
                {
                    mat.shader = halfshader;
                }
            }
            if (obj.layer == oneLayer)
            {
                Renderer renderer = obj.GetComponent<Renderer>();

                foreach (Material mat in renderer.materials)
                {
                    mat.shader = oneshader;
                }
            }
            if (obj.layer == twoLayer)
            {
                Renderer renderer = obj.GetComponent<Renderer>();

                foreach (Material mat in renderer.materials)
                {
                    mat.shader = twoshader;
                }
            }
            if (obj.layer == trafficLayer)
            {
                Renderer renderer = obj.GetComponent<Renderer>();

                foreach (Material mat in renderer.materials)
                {
                    mat.shader = trafficshader;
                }
            }
            if (obj.layer == skyLayer)
            {
                Renderer renderer = obj.GetComponent<Renderer>();

                foreach (Material mat in renderer.materials)
                {
                    mat.shader = skyshader;
                }
            }


        }
    }
}
