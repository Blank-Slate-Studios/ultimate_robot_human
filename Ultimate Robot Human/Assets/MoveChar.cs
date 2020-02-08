using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChar : MonoBehaviour
{
    float dirX;
    [SerializeField]
    float moveSpeed = 5f;
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime;
        transform.position = new Vector2 (transform.position.x + dirX, transform.position.y);
    }
}
