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
    private float maxpower = 5f;
    private int car_speed = 1;
    private Rigidbody rb;

    private Vector3 start_position = new Vector3();
    private Quaternion start_rotation = new Quaternion();

    [SerializeField]
    private TrafficManager traffic_number;



    private void Start()
    {
        start_position = transform.localPosition;
        start_rotation = transform.localRotation;

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
        transform.localPosition = start_position;
        transform.localRotation = start_rotation;
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        car_speed = 1;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var car_angle = Mathf.Floor(actions.ContinuousActions[0]*10) / 10;

        AddReward(0.5f);
        if (car_angle<1 && car_angle>-1)
        {
            AddReward(0.05f);
        }
        if (car_speed == 0)
        {
            rb.velocity = Vector3.zero;
        }
        if (car_speed == 1)
        {
            rb.AddForce(transform.forward * 30);
            transform.Rotate(0, car_angle * 1.0f, 0);
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
            AddReward(-300f);
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
