using UnityEngine;
using System.Collections;

public class FrogMove : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float wallLeft = 0.0f;
    public float wallRight = 5.0f;      
    float walkingDirection = 1.0f;
    Vector2 walkAmount;


    void Start()
    {
        wallLeft = transform.position.x - 2.5f;
        wallRight = transform.position.x + 2.5f;
    }

    void Update()
    {
        walkAmount.x = walkingDirection * moveSpeed * Time.deltaTime;
        if (walkingDirection > 0.0f && transform.position.x >= wallRight)
        {
            walkingDirection = -1.0f;
        }
        else if (walkingDirection < 0.0f && transform.position.x <= wallLeft)
        {
            walkingDirection = 1.0f;
        }
        transform.Translate(walkAmount);
    }
}