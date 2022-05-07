using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : CarController
{
    float horizontal = 0f;
    float vertical = 0f;

    private Rigidbody rbAI;

    // Start is called before the first frame update
    void Start()
    {
        rbAI = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ComputerInput(horizontal, vertical);
        Fall();
    }

    public void ComputerInput(float horizontal, float vertical)
    {
        Move(vertical);
        Turn(horizontal);
    }

    void Move(float input)
    {
        if (input > 0)
        {
            rbAI.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * speed * 10);
            //Debug.Log("Forward");
        }
        else if (input < 0)
        {
            rbAI.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * -speed * 10);
            //Debug.Log("Backward");
        }

        Vector3 localVelocity = transform.InverseTransformDirection(rbAI.velocity);
        localVelocity.x = 0;
        rbAI.velocity = transform.TransformDirection(localVelocity);
    }

    void Turn(float input)
    {
        if (input > 0)
        {
            rbAI.AddTorque(Vector3.up * turnSpeed * 10);
            //Debug.Log("Right");
            
        }
        else if (input < 0)
        {
            rbAI.AddTorque(-Vector3.up * turnSpeed * 10);
            //Debug.Log("Left");
            
        }
    }

    void Fall()
    {
        rbAI.AddForce(Vector3.down * gravityMultiplier * 10);
    }
}