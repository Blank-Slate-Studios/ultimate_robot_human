using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PlayerAgent : Agent
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

    const float k_GroundedRadius = .01f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up

    public bool lethalCol = false;
    public bool finishCol = false;

    public Rigidbody2D rigid;
    public SpawnPlayer spawner;

    [SerializeField] Transform[] spawnPoints;
    //[SerializeField] GameObject playerPrefab;
    int randomSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
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
            //Debug.Log("Death");
            //gameStop = true;
            //rigid.velocity = Vector2.zero;
            lethalCol = true;
        }
        if (colObj.gameObject.tag.Equals("Finish"))
        {
            //Debug.Log("Success");
            //gameStop = true;
            //rigid.velocity = Vector2.zero;
            finishCol = true;
        }
    }

    public Transform Target;

    public override void AgentReset()
    {
        if (lethalCol)
        {
            // Lethal collision, reset player
            rigid.velocity = Vector2.zero;
            //this.transform.position = new Vector2(-8.54f, -0.77f);
            //spawner.spawnPlayer();
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            //Instantiate(playerPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            this.transform.position = spawnPoints[randomSpawnPoint].position;
            lethalCol = false;

        }

        // Address finish line collision
        if (finishCol)
        {
            rigid.velocity = Vector2.zero;
            //this.transform.position = new Vector2(-8.54f, -0.77f);
            //spawner.spawnPlayer();
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            //Instantiate(playerPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            this.transform.position = spawnPoints[randomSpawnPoint].position;
            finishCol = false;
        }
    }

    public override void CollectObservations()
    {
        // Target and Agent positions
        AddVectorObs(Target.position);
        AddVectorObs(this.transform.position);

        // Agent velocity
        AddVectorObs(rigid.velocity.x);
        AddVectorObs(rigid.velocity.y);
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

        // Add mask to jump action


        //rigid.AddForce(new Vector2(directionX * speed, 0));
        directionY = 0;
        directionX = 0;

        // Actions size = 1 or 2?
        //Vector2 controlSignal = Vector2.zero;
        //controlSignal.x = vectorAction[0];
        //controlSignal.y = vectorAction[1];
        //rigid.AddForce(controlSignal * speed);

        // Rewards
        if (finishCol)
        {
            Debug.Log("success");
            AddReward(1.0f);
            Done();
        }

        if (lethalCol)
        {
            Debug.Log("death");
            AddReward(-0.5f);
            Done();
        }

        AddReward(-0.01f);

        if (GetCumulativeReward() <= -1.0f)
        {
            Done();
        }
    }

    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            action[1] = 1.0f;
        }
        else
        {
            action[1] = 0.0f;
        }

        //action[1] = Input.GetAxisRaw("Vertical");
        
        //action[1] = Mathf.FloorToInt(Input.GetButtonDown("Jump"));
        return action;
    }
}
