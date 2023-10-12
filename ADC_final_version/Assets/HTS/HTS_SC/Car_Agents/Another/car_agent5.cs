using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System.IO;
using System.Text;

public class car_agent5 : Agent
{
    //���� �Ķ����
    private Rigidbody rb;
    private Vector3 start_position = new Vector3();
    private Quaternion start_rotation = new Quaternion();
    private float maxpower = 1f;
    private int car_speed = 1;
    private float pre_carangle = 0; 
    private float dist;

    // ���� ���� ��ũ��Ʈ
    [SerializeField] private TrafficManager1 traffic_number;

    //csv���� �̹������� ���� �Ķ����
    private string filePath = "./Image_F/df.csv";
    private string filename = "./image_F/image/";
    private StringBuilder sb = new StringBuilder();
    private float timer = 0f;
    private bool camera_on = false;
    private float captureInterval = 0.3f;
    private int filenumber = 1;
    private float csv_car_angle;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        // ���� ��ġ �� ȸ��, ���� �߽� ����
        start_position = transform.localPosition; 
        start_rotation = transform.localRotation;
        rb.centerOfMass = new Vector3(0, 0, 0); 

        //csv���� ���� Ȯ��
        if (!File.Exists(filePath))
        {
            sb.AppendLine("image_name,speed,angle");
        }
    }
    private void Update()
    {
        //��ȣ�� ��ȣ Ȯ�� �� �� �ӵ� 1�� ����
        if (traffic_number.light_signal > 0)
        {
            car_speed = 1;
        }

        //ī�޶� �����Ӵ� �̹����� ĸó�ϱ�
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (camera_on)
            {
                camera_on = false;
            }
            else
            {
                camera_on = true;
            }
        }
        if (camera_on && timer > captureInterval)
        {
            capture_def();
            timer = 0;
        }
    }

    public override void OnEpisodeBegin()
    {
        //���� �Ķ���� �ʱ�ȭ
        transform.localPosition = start_position;
        transform.localRotation = start_rotation; 
        rb.velocity = Vector3.zero; 
        rb.angularVelocity = Vector3.zero;
        car_speed = 1;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        //���� ���Ⱒ�� �ޱ�
        var car_angle = Mathf.Floor(actions.ContinuousActions[0] * 10) / 10;
        
        csv_car_angle = Mathf.Floor((car_angle + 1) / 2 * 10f) / 10f;
        //Debug.Log(car_angle);

        //���� ó��
        AddReward(1.0f);
        //if (car_angle >=0.8f)
        //{
        //    AddReward(0.5f);
        //}
        //if (car_angle <= -0.8f)
        //{
        //    AddReward(0.5f);
        //}
        //if (car_angle >= -0.2 && car_angle <= 0.2)
        //{
        //    AddReward(0.5f);
        //}
        //else
        //{
        //    AddReward(-0.3f);
        //}


        dist = pre_carangle - car_angle;
        if (Mathf.Abs(dist) <= 0.4)
        {
            AddReward(0.01f);
        }
        pre_carangle = car_angle;

        //if (pre_carangle == 0 && car_angle == 0)
        //{
        //    AddReward(0.1f);
        //}
        //else if (pre_carangle >0.6 && car_angle >0.6)
        //{
        //    AddReward(0.1f);
        //}
        //else if(pre_carangle <-0.6 && car_angle <-0.6)
        //{
        //    AddReward(0.1f);
        //}





        //if (car_angle < 0.2 && car_angle > -0.2)
        //{
        //    AddReward(0.05f);
        //    if (car_angle < 0.1 && car_angle > -0.1)
        //    {
        //        AddReward(0.05f);
        //    }
        //}




        // ���� �ִ� �ӵ� ����
        if (car_speed == 0)
        {
            rb.velocity = Vector3.zero;
        }
        else if (car_speed == 1)
        {
            rb.AddForce(transform.forward * 30);
            transform.Rotate(0, car_angle * 1.1f, 0);
        }

        // ���� ���Ⱒ�� ����
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
        //�г�Ƽ ����
        if (other.gameObject.CompareTag("floor")) 
        {
            AddReward(-500f); 
            EndEpisode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //�г�Ƽ ����
        if (other.gameObject.CompareTag("floor")) 
        {
            AddReward(-500f);
            EndEpisode(); 
        }

        //��ȣ�� ���� �� �ӵ� ����
        if (other.gameObject.CompareTag("lightCross") && traffic_number.light_signal == 0) 
        {
            car_speed = 0; 
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var angle_action = actionsOut.ContinuousActions; 
        angle_action[0] = Input.GetAxis("Horizontal"); 
    }

    void capture_def()
    {
        string image_number_temp = ("image_" + filenumber.ToString("D7"));
        ScreenCapture.CaptureScreenshot(filename + image_number_temp + ".png");
        string newLine = string.Format("{0},{1},{2}", image_number_temp,car_speed, csv_car_angle);
        sb.Clear();
        sb.AppendLine(newLine);
        File.AppendAllText(filePath, sb.ToString());
        filenumber += 1;
        Debug.Log("��Ĭ");
    }

}
