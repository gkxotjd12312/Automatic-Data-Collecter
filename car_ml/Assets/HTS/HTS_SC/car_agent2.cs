using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System.IO;
using System.Text;

public class car_agent2 : Agent
{

    private string filename = "./image_F/image/";
    private int filenumber = 1;
    private bool camera_on = false;
    float captureInterval = 0.1f;
    float timer = 0f;

    string filePath = "./Image_F/df.csv";
    StringBuilder sb = new StringBuilder();
    float csv_car_angle;

    int speed_value = 0;
    private float maxpower = 5f;
    private float power = 1250f;
    private float rot = 20f;
    private int car_speed = 1;
    private Rigidbody rb;

    private Vector3 wheel_position;
    private Quaternion wheel_rotation;

    [SerializeField]
    private TrafficManager traffic_number;
    int traffic_number_send=0;



    private void Start()
    {
        if (!File.Exists(filePath))
        {
            sb.AppendLine("image_name,drive_mod,speed,angle");
        }
        rb = this.GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, 0, 0);
    }
    private void Update()
    {
        if(traffic_number.light_signal>0)
        {
            car_speed = 1;
        }
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        car_speed = 1;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var car_angle = Mathf.Floor(actions.ContinuousActions[0]*10) / 10;
        csv_car_angle = Mathf.Floor((car_angle+1)/2 *10f)/10f;

        if (car_speed == 0)
        {
            rb.velocity = Vector3.zero;
        }
        if (car_speed == 1)
        {
            rb.AddForce(transform.forward * 15);
            transform.Rotate(0, car_angle * 0.8f, 0);
        }

        if (rb.velocity.magnitude >= maxpower / 5)
        {
            if (rb.velocity.magnitude >= maxpower)
            {
                rb.velocity = rb.velocity.normalized * maxpower;
            }
        }
        
        

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("floor"))
        {
            AddReward(-100f);
            EndEpisode();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("lightCross") && traffic_number.light_signal == 0)
        {
            car_speed = 0;
        }
        if (other.gameObject.CompareTag("block"))
        {
            AddReward(-90f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var angle_action = actionsOut.ContinuousActions;
        angle_action[0] = Input.GetAxis("Horizontal");
    }
}
