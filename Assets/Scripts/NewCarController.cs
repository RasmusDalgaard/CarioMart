using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCarController : MonoBehaviour
{
    //Input variables
    private float horizontalInput;
    private float verticalInput;

    //Steering
    private float steeringAngle;
    [SerializeField] private float maxSteeringAngle = 30;

    //Wheels
    [SerializeField] private WheelCollider frontLeftW, frontRightW;
    [SerializeField] private WheelCollider rearLeftW, rearRightW;
    [SerializeField] private Transform frontLeftT, frontRightT;
    [SerializeField] private Transform rearLeftT, rearRightT;

    //Motorforce
    public float motorForce = 50;

    //Sound
    public AudioSource driving;
    public AudioSource idle;
    public AudioClip engine;
    public AudioClip engineIdle;

    //Janus
    private Rigidbody rb;
    private float boostTimer;
    private bool boosting;



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

    private void ModifyCarSpeed()
    {
        if (boosting)                                   //Boosting is default false. It is set to true when the car collides with the object
        {
            boostTimer += Time.deltaTime;
            if (boostTimer >= 3)                        //BoostTimer >= seconds the boosting effect lasts.
            {
                rb = GetComponent<Rigidbody>();
                rb.mass = 400;
                boostTimer = 0;
                boosting = false;                       //Boosting is set to false to make the car normal again.

            }
        }
    }



    private void FixedUpdate()
    {
        //Sounds

        if (frontLeftW.motorTorque > 0 && !driving.isPlaying)
        {
            idle.Stop();
            driving.PlayOneShot(engine);
        }

        if (frontLeftW.motorTorque == 0 && !idle.isPlaying)
        {
            driving.Stop();
            idle.PlayOneShot(engineIdle);
        }

        Debug.Log(frontLeftW.motorTorque);

        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
        ModifyCarSpeed();
    }

    //Trigger effect when the car collides with the object.
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SlowSpeed")
        {
            boosting = true;
            rb = GetComponent<Rigidbody>();
            rb.mass = 500000;
        }
        if (other.tag == "SpeedBoost")
        {
            boosting = true;
            rb = GetComponent<Rigidbody>();
            rb.mass = 130;
            // Destroy(other.gameObject);
        }
    }


}
