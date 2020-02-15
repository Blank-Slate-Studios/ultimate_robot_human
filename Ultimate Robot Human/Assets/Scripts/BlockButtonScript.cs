using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockButtonScript : MonoBehaviour
{
    public GameObject blockPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void spawnBlock()
    {
        GameObject block = Instantiate(blockPrefab) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
