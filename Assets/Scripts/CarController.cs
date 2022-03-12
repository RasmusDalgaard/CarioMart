using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float moveInput;
    private float turnInput;
    private bool isCarGrounded;

    [SerializeField] private float airDrag;
    [SerializeField] private float groundDrag;

    [SerializeField] private float forwardSpeed;
    [SerializeField] private float reverseSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody sphereRB;


    void Start()
    {
        //Detach rigidbody from car
        sphereRB.transform.parent = null;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");

        //If forward key pressed, moveInput = forward speed, if reverse key pressed, moveInput = reverseSpeed
        moveInput *= moveInput > 0 ? forwardSpeed : reverseSpeed;

        
        //Set cars position to spheres position
        transform.position = sphereRB.transform.position;

        //Set cars rotation Input.GetAxisRaw returns 0 if 'W' or 'S' isn't pressed.
        float newRotation = turnInput * turnSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        transform.Rotate(0, newRotation, 0, Space.World);

        //Raycast ground check
        RaycastHit hit;
        isCarGrounded = Physics.Raycast(transform.position, -transform.up, out hit, groundLayer);

        //rotate car to be parallel to ground
        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        //Set sphere drag based on grounded or in air.
        if (isCarGrounded)
        {
            sphereRB.drag = groundDrag;
        }
        else
        {
            sphereRB.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        if (isCarGrounded)
        {
            //Move car
            sphereRB.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
        }
        else
        {
            //add extra gravity
            sphereRB.AddForce(transform.up * -20f);
        }

        
    }
}
