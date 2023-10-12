using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class floor_create : MonoBehaviour
{
    int obj_number;
    int obj_size;
    int noise_number;

    float world_time = 0;

    public bool half = false;
    public bool one = false;
    public bool two = false;
    public bool forest = false;

    Object[] obj_prefabs;
    List<GameObject> spawnedObjects = new List<GameObject>();

    private void Start()
    {
        build_action_def();
    }

    private void Update()
    {
        world_time += Time.deltaTime;

        if (world_time > 120)
        {
            build_action_def();
            world_time = 0;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            build_action_def();
        }
    }

    private void build_action_def()
    {
        foreach (var obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();


        if (half)
        {
            obj_prefabs = Resources.LoadAll("Prefabs/Buildings/half");
            obj_size = 7;
            build_def();
        }
        if (one)
        {
            obj_prefabs = Resources.LoadAll("Prefabs/Buildings/one");
            obj_size = 10;
            build_def();
        }
        if (two)
        {
            obj_prefabs = Resources.LoadAll("Prefabs/Buildings/two");
            obj_size = 20;
            build_two_def();
        }
        if (forest)
        {
            obj_prefabs = Resources.LoadAll("Prefabs/Environment");
            obj_size = 2;
            forest_def();
        }

        void build_def()
        {
            for (int i = (int)(-((transform.localScale.x / 2) * 10) + transform.position.x + 8); i < (int)(((transform.localScale.x / 2) * 10) + transform.position.x - obj_size / 2); i += obj_size)
            {
                for (int j = (int)(-((transform.localScale.z / 2) * 10) + transform.position.z + 8); j < (int)(((transform.localScale.z / 2) * 10) + transform.position.z - obj_size / 2); j += obj_size)
                {
                    noise_number = Random.Range(-2, 2);
                    obj_number = Random.Range(0, obj_prefabs.Length);
                    GameObject temp = Instantiate(obj_prefabs[obj_number], new Vector3(i + noise_number, 0, j + noise_number), Quaternion.identity) as GameObject;
                    spawnedObjects.Add(temp);
                }
            }
        }
        void build_two_def()
        {
            for (int i = (int)(-((transform.localScale.x / 2) * 10) + transform.position.x + 15); i < (int)(((transform.localScale.x / 2) * 10) + transform.position.x - obj_size / 2) - 8; i += obj_size)
            {
                for (int j = (int)(-((transform.localScale.z / 2) * 10) + transform.position.z + 15); j < (int)(((transform.localScale.z / 2) * 10) + transform.position.z - obj_size / 2) - 8; j += obj_size)
                {
                    noise_number = Random.Range(-2, 2);
                    obj_number = Random.Range(0, obj_prefabs.Length);
                    GameObject temp = Instantiate(obj_prefabs[obj_number], new Vector3(i + noise_number, 0, j + noise_number), Quaternion.identity) as GameObject;
                    spawnedObjects.Add(temp);
                }
            }
        }
        void forest_def()
        {
            for (int i = (int)(-((transform.localScale.x / 2) * 10) + transform.position.x + 2); i < (int)(((transform.localScale.x / 2) * 10) + transform.position.x); i += obj_size)
            {
                for (int j = (int)(-((transform.localScale.z / 2) * 10) + transform.position.z + 2); j < (int)(((transform.localScale.z / 2) * 10) + transform.position.z); j += obj_size)
                {
                    noise_number = Random.Range(-1, 1);
                    obj_number = Random.Range(0, obj_prefabs.Length);
                    GameObject temp = Instantiate(obj_prefabs[obj_number], new Vector3(i + noise_number, 0, j + noise_number), Quaternion.identity) as GameObject;
                    spawnedObjects.Add(temp);
                }
            }
        }
    }
}
