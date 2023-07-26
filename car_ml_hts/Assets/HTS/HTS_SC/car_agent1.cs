using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System.IO;
using System.Text;

public class car_agent1 : Agent
{
    [SerializeField]
    private WheelCollider[] wheels = new WheelCollider[4];
    [SerializeField]
    private Transform[] tires = new Transform[4];

    private string filename = "./image_F/image/";
    private int filenumber = 1;
    private bool camera_on = false;
    float captureInterval = 0.1f;
    float timer = 0f;

    string filePath = "./Image_F/df.csv";
    StringBuilder sb = new StringBuilder();
    float csv_car_angle;

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

    public override void OnEpisodeBegin()
    {
        //transform.localPosition = new Vector3(-60, 0, -6);
        transform.localPosition = new Vector3(0, 0, 0);
        //transform.rotation = new Quaternion(0.00000f, 0.70711f, 0.00000f, 0.70711f);
        //transform.rotation = new Quaternion(0.00000f, 1.00000f, 0.00000f, 0.00000f);
        transform.rotation = Quaternion.identity;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        car_speed = 1;
    }
    private void Update()
    {
        traffic_number_send = traffic_number.light_signal;
        if (traffic_number_send >0)
        {
            car_speed = 1;
        }

        timer += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.C))
        {
            if(camera_on)
            {
                camera_on = false;
            }
            else
            {
                camera_on = true;
            }
        }

        if (camera_on && timer>captureInterval)
        {
            capture_def();
            timer = 0;
        }
    }
    void capture_def()
    {
        string image_number_temp = ("image_"+filenumber.ToString("D7"));
        ScreenCapture.CaptureScreenshot(filename+image_number_temp+".png");
        string newLine = string.Format("{0},{1},{2},{3}", image_number_temp, 2, 0.8, csv_car_angle);
        sb.Clear();
        sb.AppendLine(newLine);
        File.AppendAllText(filePath, sb.ToString());

        filenumber += 1;
        Debug.Log("ÂûÄ¬");
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        //traffic_number_send = traffic_number.light_signal;
        //sensor.AddObservation(traffic_number_send);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        var car_angle = Mathf.Floor(actions.ContinuousActions[0]*10) / 10;
        csv_car_angle = Mathf.Floor((car_angle+1)/2 *10f)/10f;

        if (-0.2<car_angle & car_angle<0.2)
        {
            AddReward(0.5f);
        }
        AddReward(1f);

        if (car_speed == 0)
        {
            rb.velocity = Vector3.zero;
        }

        if (rb.velocity.magnitude >= maxpower / 5)
        {
            if (rb.velocity.magnitude >= maxpower)
            {
                rb.velocity = rb.velocity.normalized * maxpower;
            }
        }
        for (int i=2; i<4; i++)
        {
            wheels[i].motorTorque = car_speed * power;
            
            wheels[i].GetWorldPose(out wheel_position, out wheel_rotation);
            tires[i].rotation = wheel_rotation;
        }
        for(int i=0; i<2; i++)
        {
            wheels[i].steerAngle = car_angle * rot;
            wheels[i].GetWorldPose(out wheel_position, out wheel_rotation);
            tires[i].rotation = wheel_rotation;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("lightCross"))
        {
            car_speed = 0;
        }
        if (other.gameObject.CompareTag("floor"))
        {
            AddReward(-100f);
            EndEpisode();
        }
        if(other.gameObject.CompareTag("block"))
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
