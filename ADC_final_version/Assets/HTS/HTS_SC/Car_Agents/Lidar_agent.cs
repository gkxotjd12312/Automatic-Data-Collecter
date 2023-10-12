using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System.IO;
using System.Text;

public class Lidar_agent : Agent
{
    // �ڵ����� �� ������ Ÿ�̾� ������ ��� �迭
    [SerializeField] private WheelCollider[] wheels = new WheelCollider[4];
    [SerializeField] private Transform[] tires = new Transform[4];

    // �ʱ� ��ġ�� ȸ�� ������ �����ϴ� ����
    private Vector3 start_position = new Vector3();
    private Quaternion start_rotation = new Quaternion();

    // �̹��� ���� ��ο� ���� ��ȣ, ī�޶� Ȱ��ȭ ����, ĸó ���� ���� ����
    private string folderPath;
    private string imageFilePath;

    private int filenumber = 1;
    private bool camera_on = false;
    float captureInterval = 0.3f;
    float timer = 0f;

    // CSV ���� ���� ��ο� ������ ó���� ���� StringBuilder ����
    private string filePath;
    private StringBuilder sb = new StringBuilder();
    private float csv_car_angle;

    // �ڵ��� ��� ���Ǵ� ������
    private float maxpower = 5f;
    private float power = 1250f;
    private float rot = 17.5f;
    private int car_speed = 1;
    Rigidbody rb;
    private float pre_angle = 0;

    // ������ ��ġ�� ȸ�� ���� ���� ����
    private Vector3 wheel_position;
    private Quaternion wheel_rotation;

    // TrafficManager1 ��ũ��Ʈ���� �޾ƿ��� ��ȣ ������ ����Ǵ� ����
    [SerializeField] private TrafficManager1 traffic_number;
    private int traffic_number_send = 0;

    void Awake()
    {
        start_position = new Vector3(-19.1599998f, 0, 61.8499985f);
        start_rotation = Quaternion.Euler(0, 180, 0);

        //start_position = transform.localPosition;
        //start_rotation = transform.localRotation;

        // ���� ��θ� persistentDataPath�� ������� ����
        folderPath = Path.Combine(Application.persistentDataPath, "Lidar_F");
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

        filePath = Path.Combine(folderPath, "Lidar_data.csv");

        if (!File.Exists(filePath))
        {
            sb.AppendLine("image_name,drive_mod,speed,angle");
        }

        rb = this.GetComponent<Rigidbody>(); // Rigidbody ������Ʈ�� ������ rb ������ �Ҵ�
        rb.centerOfMass = new Vector3(0, 0, 0);

    }

    private void Update()
    {
        traffic_number_send = traffic_number.light_signal;

        if (traffic_number_send > 0)
        {
            car_speed = 1;
        }

        timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.C))
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
        transform.localPosition = start_position;
        transform.localRotation = start_rotation;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        car_speed = 1;
        pre_angle = 0;
    }
 
    public override void OnActionReceived(ActionBuffers actions)
    {
        var car_angle = Mathf.Floor(actions.ContinuousActions[0] * 10) / 10;
        csv_car_angle = Mathf.Floor((car_angle + 1) / 2 * 10f) / 10f;

        if (car_speed == 0)
        {
            rb.velocity = Vector3.zero;
            csv_car_angle = 0.5f;
        }

        AddReward(1.0f);

        if (Mathf.Abs(pre_angle - car_angle) <= 0.2)
        {
            AddReward(0.001f);
        }

        pre_angle = car_angle;

        if (rb.velocity.magnitude >= maxpower / 5)
        {
            if (rb.velocity.magnitude >= maxpower)
            {
                rb.velocity = rb.velocity.normalized * maxpower;
            }
        }

        for (int i = 2; i < 4; i++)
        {
            wheels[i].motorTorque = car_speed * power;

            wheels[i].GetWorldPose(out wheel_position, out wheel_rotation);
            tires[i].rotation = wheel_rotation;
        }

        for (int i = 0; i < 2; i++)
        {
            wheels[i].steerAngle = car_angle * rot;
            wheels[i].GetWorldPose(out wheel_position, out wheel_rotation);
            tires[i].rotation = wheel_rotation;
        }
    }

    void capture_def()
    {
        string imageName = $"lidar_{filenumber:D7}";
        string imagePath = Path.Combine(imageFilePath, $"{imageName}.png");

        ScreenCapture.CaptureScreenshot(imagePath);

        string newLine = string.Format("{0},{1},{2}", imageName, car_speed, csv_car_angle);
        sb.Clear();
        sb.AppendLine(newLine);
        File.AppendAllText(filePath, sb.ToString());

        filenumber++;

        Debug.Log(csv_car_angle);
        Debug.Log("��Ĭ");
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
}
