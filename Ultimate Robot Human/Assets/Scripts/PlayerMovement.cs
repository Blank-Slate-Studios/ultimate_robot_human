using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
<<<<<<< HEAD:Ultimate Robot Human/Assets/PlayerMovement.cs
    public GameObject gameOverText, winText, restartButton;
=======
    public GameObject gameOverText, winText, restartButton, nextLevelButton;
    public bool gameStop = false;
    private Rigidbody2D rigid;
>>>>>>> f411d823abad7bd5095fc1ba038ee1d2db75756e:Ultimate Robot Human/Assets/Scripts/PlayerMovement.cs

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD:Ultimate Robot Human/Assets/PlayerMovement.cs
        gameOverText.SetActive(false);
        winText.SetActive(false);
        restartButton.SetActive(false);
=======
        rigid = GetComponent<Rigidbody2D>();
        gameOverText.SetActive(false);
        winText.SetActive(false);
        restartButton.SetActive(false);
        nextLevelButton.SetActive(false);
>>>>>>> f411d823abad7bd5095fc1ba038ee1d2db75756e:Ultimate Robot Human/Assets/Scripts/PlayerMovement.cs
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
            gameOverText.SetActive(true);
            restartButton.SetActive(true);
<<<<<<< HEAD:Ultimate Robot Human/Assets/PlayerMovement.cs
=======
            gameStop = true;
            rigid.velocity = Vector2.zero;
>>>>>>> f411d823abad7bd5095fc1ba038ee1d2db75756e:Ultimate Robot Human/Assets/Scripts/PlayerMovement.cs
        }
        if (colObj.gameObject.tag.Equals("Finish"))
        {
            Debug.Log("Success");
            winText.SetActive(true);
            restartButton.SetActive(true);
<<<<<<< HEAD:Ultimate Robot Human/Assets/PlayerMovement.cs
=======
            nextLevelButton.SetActive(true);
            gameStop = true;
            rigid.velocity = Vector2.zero;
>>>>>>> f411d823abad7bd5095fc1ba038ee1d2db75756e:Ultimate Robot Human/Assets/Scripts/PlayerMovement.cs
        }
    }
}
