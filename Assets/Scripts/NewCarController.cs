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

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }
}
