using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineButtonScript : MonoBehaviour
{
    public GameObject finishLinePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void spawnFinishLine()
    {
        GameObject finishLine = Instantiate(finishLinePrefab) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
