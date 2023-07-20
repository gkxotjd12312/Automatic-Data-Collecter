using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class car_agent : Agent
{
    [SerializeField]
    private WheelCollider[] wheels = new WheelCollider[4];
    [SerializeField]
    private Transform[] tires = new Transform[4];


    private float maxpower = 5f;
    private float power = 250f;
    private float rot = 20f;
    private Rigidbody rb;

    private Vector3 wheel_position;
    private Quaternion wheel_rotation;
    float temp_reward;

    private void Start()
    {
 
        this.rb = this.GetComponent<Rigidbody>();
        this.rb.centerOfMass = new Vector3(0, 0, 0);
    }

    public override void OnEpisodeBegin()
    {
        this.transform.localPosition = new Vector3(0, 0, 0);
        this.transform.rotation = Quaternion.identity;
        this.rb.velocity = Vector3.zero;
        this.rb.angularVelocity = Vector3.zero;
        temp_reward = 0f;


    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(rb.velocity);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        var car_angle = actions.ContinuousActions[0];
        var car_speed = actions.ContinuousActions[1];
        
        if (car_speed <= 0)
        {
            car_speed = 0;
        }
        if(rb.velocity.magnitude >= maxpower/2)
        {
            
            AddReward(0.1f);
            temp_reward += 0.1f;
            if(rb.velocity.magnitude >= maxpower)
            {
                AddReward(0.5f);
                
                temp_reward += 0.5f;
                rb.velocity = rb.velocity.normalized * maxpower;
            }
        }
        for(int i =2; i< wheels.Length; i++)
        {
            wheels[i].motorTorque = car_speed  * power;

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
        
        if (other.gameObject.CompareTag("floor"))
        {
            Debug.Log(temp_reward);
            AddReward(-100f);
            EndEpisode();
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var action = actionsOut.ContinuousActions;
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
    }

}
