using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;
    
    [SerializeField] private WheelCollider frontLeftW, frontRightW;
    [SerializeField] private WheelCollider rearLeftW, rearRightW;
    [SerializeField] private Transform frontLeftT, frontRightT;
    [SerializeField] private Transform rearLeftT, rearRightT;
    [SerializeField] private float maxSteeringAngle = 30;
    [SerializeField] private float motorForce = 50;

    public float mass;   //speed of the car
    private Rigidbody rb;
    private float boostTimer;
    private bool boosting;


    void Start()
    {
        boostTimer = 0;
        boosting = false;

    }
    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        steeringAngle = maxSteeringAngle * horizontalInput;
        frontLeftW.steerAngle = steeringAngle;
        frontRightW.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        frontLeftW.motorTorque = verticalInput * motorForce;
        frontRightW.motorTorque = verticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftW, frontLeftT);
        UpdateWheelPose(frontRightW, frontRightT);
        UpdateWheelPose(rearLeftW, rearLeftT);
        UpdateWheelPose(rearRightW, rearRightT);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        Vector3 pos = transform.position;
        Quaternion quat = transform.rotation;

        collider.GetWorldPose(out pos, out quat);

        transform.position = pos;
        transform.rotation = quat;
    }

    private void SlowSpeed()
    {
        if (boosting)
        {
            boostTimer += Time.deltaTime;
            if (boostTimer >= 3)
            {
                rb = GetComponent<Rigidbody>();
                rb.mass = 400;
                boostTimer = 0;
                boosting = false;

            }
        }
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
        SlowSpeed();



    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SlowSpeed")
        {
            boosting = true;
            rb = GetComponent<Rigidbody>();
            rb.mass = 40000;
        }
    }
}
