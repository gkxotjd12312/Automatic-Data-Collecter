using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class random_sky_box : MonoBehaviour
{
    Material[] sky_m;
    MeshRenderer renderer;
    Material[] currentmaterial;

    float world_time = 0;


    // Start is called before the first frame update
    void Start()
    {
        sky_m = Resources.LoadAll<Material>("skybox");

        renderer = GetComponent<MeshRenderer>();
        currentmaterial = renderer.materials;

        ChangeSky();
    }
    private void Update()
    {
        world_time += Time.deltaTime;

        if (world_time > 120)
        {
            ChangeSky();
            world_time = 0;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeSky();
        }
    }

    public void ChangeSky()
    {
        int random_number = Random.Range(0, sky_m.Length);
        currentmaterial[0] = sky_m[random_number];
        renderer.materials = currentmaterial;
    }
}
