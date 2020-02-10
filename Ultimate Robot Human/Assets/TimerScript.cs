using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    private Text newTimerText;
    private float time = 0.00f;
    GameObject player;
    PlayerMovement playerMovement;
    
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        newTimerText = GetComponent<Text>();
        newTimerText.text = time.ToString("0.00");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (!playerMovement.gameStop)
        {
            time = Time.timeSinceLevelLoad;
            newTimerText.text = time.ToString("0.00");
        }           
    }
}
