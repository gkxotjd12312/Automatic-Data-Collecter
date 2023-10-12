using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System.IO;
using System.Text;

public class Segment_agent : Agent
{
    // 자동차의 네 바퀴와 타이어 정보를 담는 배열
    [SerializeField] private WheelCollider[] wheels = new WheelCollider[4];
    [SerializeField] private Transform[] tires = new Transform[4];

    // 초기 위치와 회전 정보를 저장하는 변수
    private Vector3 start_position = new Vector3();
    private Quaternion start_rotation = new Quaternion();

    // 자동차 제어에 사용되는 변수들
    private float maxpower = 5f;
    private float power = 1250f;
    private float rot = 17.5f;
    private int car_speed = 1;
    Rigidbody rb;
    private float pre_angle = 0;

    // 바퀴의 위치와 회전 정보 저장 변수
    private Vector3 wheel_position;
    private Quaternion wheel_rotation;

    // TrafficManager1 스크립트에서 받아오는 신호 정보와 연결되는 변수
    [SerializeField] private TrafficManager1 traffic_number;
    private int traffic_number_send = 0;

    void Awake()
    {
        start_position = new Vector3(-19.1599998f, 0, 61.8499985f);
        start_rotation = Quaternion.Euler(0, 180, 0);

        //start_position = transform.localPosition;
        //start_rotation = transform.localRotation;

        rb = this.GetComponent<Rigidbody>(); // Rigidbody 컴포넌트를 가져와 rb 변수에 할당
        rb.centerOfMass = new Vector3(0, 0, 0);

    }

    private void Update()
    {
        traffic_number_send = traffic_number.light_signal;

        if (traffic_number_send > 0)
        {
            car_speed = 1;
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

        if (car_speed == 0)
        {
            rb.velocity = Vector3.zero;
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

    private void OnCollisionEnter(Collision other)
    {
        //패널티 설정
        if (other.gameObject.CompareTag("floor"))
        {
            AddReward(-500f);
            EndEpisode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //패널티 설정
        if (other.gameObject.CompareTag("floor"))
        {
            AddReward(-500f);
            EndEpisode();
        }

        //신호등 감지 및 속도 제어
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
