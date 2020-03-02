using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PlayerAgent : Agent
{
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
        Debug.Log(rigid.velocity);
        Debug.Log(rigid.velocity.x);        
    }

    void OnCollisionEnter2D(Collision2D colObj)
    {
        if (colObj.gameObject.tag.Equals("Lethal"))
        {
            //Debug.Log("Death");
            //gameOverText.SetActive(true);
            //restartButton.SetActive(true);
            //gameStop = true;
            //rigid.velocity = Vector2.zero;
            lethalCol = true;
        }
        if (colObj.gameObject.tag.Equals("Finish"))
        {
            //Debug.Log("Success");
            //winText.SetActive(true);
            //restartButton.SetActive(true);
            //nextLevelButton.SetActive(true);
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

        // Address finish line collision later
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

    public float speed = 10f;
    public override void AgentAction(float[] vectorAction)
    {
        // Actions size = 1 or 2?
        Vector2 controlSignal = Vector2.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.y = vectorAction[1];
        rigid.AddForce(controlSignal * speed);

        // Rewards
        if (finishCol)
        {
            Debug.Log("success");
            SetReward(1.0f);
            Done();
        }

        if (lethalCol)
        {
            Debug.Log("death");
            SetReward(-0.5f);
            Done();
        }
    }

    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }
}
