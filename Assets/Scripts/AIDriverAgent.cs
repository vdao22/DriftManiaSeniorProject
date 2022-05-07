using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AIDriverAgent : Agent
{
    [SerializeField] private ManageCheckpoints manageCheckpoints;
    //[SerializeField] private Transform spawn;

    private AIController aiController;

    private void Awake()
    {
        aiController = GetComponent<AIController>();
    }

    private void Start()
    {
        manageCheckpoints.PlayerCorrectCheckpoint += ManageCheckpoints_PlayerCorrectCheckpoint;
        manageCheckpoints.PlayerWrongCheckpoint += ManageCheckpoints_PlayerWrongCheckpoint;
    }
    
    private void Update() //make it go faster
    {
        AddReward(-0.8f);
    }
    
    private void ManageCheckpoints_PlayerCorrectCheckpoint(object sender, System.EventArgs e)
    {
        AddReward(1f);
        //Debug.Log("Reward");
    }

    private void ManageCheckpoints_PlayerWrongCheckpoint(object sender, System.EventArgs e)
    {
        AddReward(-1f);
        Debug.Log("Wrong way");
    }

    private void OnCollisionEnter(Collision collision) // When it collides with an object
    {
        if(collision.gameObject.tag == "Wall")
        {
            AddReward(-0.5f);
            //Debug.Log("punishment");
        }
        else if(collision.gameObject.tag == "Player")
        {
            AddReward(-1.5f);
        }
        else if(collision.gameObject.tag == "Computer")
        {
            AddReward(-1f);
        }
    }

    private void OnCollisionStay(Collision collision) // every update it stays collided with object
    { 
        
        if(collision.gameObject.tag == "Wall")
        {
            AddReward(-0.2f);
            Debug.Log("punishment");
        }
        else if (collision.gameObject.tag == "Player")
        {
            AddReward(-0.5f);
        }
        else if(collision.gameObject.tag == "Computer")
        {
            AddReward(-0.3f);
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float horizontal = 0f;
        float vertical = 0f;

        switch (actions.DiscreteActions[0]) // Move Forward
        {
            // 1 = key W, 2 = key S, 0 = No Input
            case 0:
                vertical = 0f;
                break;
            case 1:
                vertical = 1f;
                break;
            case 2:
                vertical = -1f;
                break;
        }

        switch (actions.DiscreteActions[1]) // turn
        {
            // 1 = Key D, 2 = Key A, 0 = No Input
            case 0:
                horizontal = 0f;
                break;
            case 1:
                horizontal = 1f;
                break;
            case 2:
                horizontal = -1f;
                break;
        }

        aiController.ComputerInput(horizontal, vertical);
    }

    public override void Heuristic(in ActionBuffers actionsOut) // Debug purposes can control computer car with direction buttons
    {
        int vertical = 0;
        if (Input.GetKey(KeyCode.W)) vertical = 1;
        if (Input.GetKey(KeyCode.S)) vertical = 2;

        int horizontal = 0;
        if (Input.GetKey(KeyCode.D)) horizontal = 1;
        if (Input.GetKey(KeyCode.A)) horizontal = 2;

        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = vertical;
        discreteActions[1] = horizontal;
    }
}