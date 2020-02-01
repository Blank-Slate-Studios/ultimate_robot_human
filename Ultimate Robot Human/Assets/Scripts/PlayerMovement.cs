using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController2D controller;
	public float runSpeed = 40f;
	float horizontalMove = 0f;
    bool jump = false;
    bool holdingJump = false;

    private float jumpTimeCounter = 0;
    public float jumpTime;

    // Update is called once per frame
    void Update()
    {
    	horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(Input.GetKey(KeyCode.Space)) {
                if(jumpTimeCounter < jumpTime) {
                    jumpTimeCounter += Time.deltaTime;
                }
                holdingJump = true;
        }

        if(Input.GetButtonUp("Jump")) {
            jump = true;
            holdingJump = false;
        }
        
    }

    void FixedUpdate() {
    	controller.Move(horizontalMove * Time.fixedDeltaTime, jump, holdingJump, jumpTimeCounter);
        if(jump) {
            jumpTimeCounter = 0;
        }
        jump = false;
    }
}
