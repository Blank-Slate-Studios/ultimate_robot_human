using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    public GameObject gameOverText, winText, restartButton, nextLevelButton;
    public bool gameStop = false;
    private Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        gameOverText.SetActive(false);
        winText.SetActive(false);
        restartButton.SetActive(false);
        nextLevelButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        if (!gameStop)
        {
            // Move character
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        }
    }

    void OnCollisionEnter2D(Collision2D colObj)
    {
        if (colObj.gameObject.tag.Equals("Lethal"))
        {
            Debug.Log("Death");
            //gameOverText.SetActive(true);
            //restartButton.SetActive(true);
            //gameStop = true;
            rigid.velocity = Vector2.zero;
            Destroy(gameObject);
        }
        if (colObj.gameObject.tag.Equals("Finish"))
        {
            Debug.Log("Success");
            winText.SetActive(true);
            restartButton.SetActive(true);
            nextLevelButton.SetActive(true);
            gameStop = true;
            rigid.velocity = Vector2.zero;
        }
    }
}
