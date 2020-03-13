using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PlayerAgent : Agent
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = true;                          // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings

    const float k_GroundedRadius = .1f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true; // Which way is player facing
    private Vector3 m_Velocity = Vector3.zero;
    
    public Rigidbody2D rigid;
    
    Vector3[] playerSpawnPoints = new Vector3[4];
    Vector3[] finishSpawnPoints = new Vector3[9];
    int randomSpawnPoint;
    int randomFinishSpawnPoint;

    public Collider[] hitGroundColliders = new Collider[3];

    public override void InitializeAgent()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        rigid = GetComponent<Rigidbody2D>();

        playerSpawnPoints[0] = new Vector3(-5.47f, 0.53f, 0);
        playerSpawnPoints[1] = new Vector3(4.48f, 0.53f, 0);
        playerSpawnPoints[2] = new Vector3(4.48f, 6.51f, 0);
        playerSpawnPoints[3] = new Vector3(-5.47f, 6.51f, 0);

        finishSpawnPoints[0] = new Vector3(-0.503f, 2.52f, 0);
        finishSpawnPoints[1] = new Vector3(-7.49f, 5.55f, 0);
        finishSpawnPoints[2] = new Vector3(-3.52f, 5.55f, 0);
        finishSpawnPoints[3] = new Vector3(2.47f, 5.55f, 0);
        finishSpawnPoints[4] = new Vector3(6.46f, 5.55f, 0);
        finishSpawnPoints[5] = new Vector3(6.46f, -0.46f, 0);
        finishSpawnPoints[6] = new Vector3(2.51f, -0.46f, 0);
        finishSpawnPoints[7] = new Vector3(-3.48f, -0.46f, 0);
        finishSpawnPoints[8] = new Vector3(-7.43f, -0.46f, 0);
    }

    public bool DoGroundCheck()
    {
        return false;
    }

    private bool isGrounded()
    {
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    void OnCollisionEnter2D(Collision2D colObj)
    {
        if (colObj.gameObject.tag.Equals("Lethal"))
        {
            SetReward(-1f);
        }
        if (colObj.gameObject.tag.Equals("Finish"))
        {
            SetReward(1f);
        }
        Done();
    }

    public Transform Target;

    public override void AgentReset()
    {
        randomSpawnPoint = Random.Range(0, playerSpawnPoints.Length);
        randomFinishSpawnPoint = Random.Range(0, finishSpawnPoints.Length);
        
        rigid.velocity = Vector2.zero;
        this.transform.localPosition = playerSpawnPoints[randomSpawnPoint];
        Target.localPosition = finishSpawnPoints[randomFinishSpawnPoint];
    }

    public override void CollectObservations()
    {
        // Target and Agent positions
        AddVectorObs(Target.localPosition);
        AddVectorObs(this.transform.localPosition);

        // Agent velocity
        AddVectorObs(rigid.velocity.x);
        AddVectorObs(rigid.velocity.y);

        // Raycast observations?
    }

    public float speed = 40f;
    private int directionX = 0;
    private int directionY = 0;
    public override void AgentAction(float[] vectorAction)
    {
        int movement = Mathf.FloorToInt(vectorAction[0]);
        int jump = Mathf.FloorToInt(vectorAction[1]);

        if (movement == 1) { directionX = -1; }
        if (movement == 2) { directionX = 1; }
        // Look up index in jump action list
        if (jump == 1 && isGrounded()) { directionY = 1; }

        rigid.AddForce(new Vector2(directionX * speed, directionY * m_JumpForce));

        // Add mask to jump action?


        //rigid.AddForce(new Vector2(directionX * speed, 0));
        directionY = 0;
        directionX = 0;
        
        // Rewards
        AddReward(-0.001f);

        if (GetCumulativeReward() <= -1f)
        {
            Done();
        }
    }

    public override float[] Heuristic()
    {
        var action = new float[2];

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            action[0] = 1f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            action[0] = 2f;
        }
        action[1] = Input.GetKey(KeyCode.Space) ? 1.0f : 0.0f;
        
        return action;
    }
}
