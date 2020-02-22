using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeButtonScript : MonoBehaviour
{
    public GameObject spikePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void spawnSpike()
    {
        GameObject spike = Instantiate(spikePrefab) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
