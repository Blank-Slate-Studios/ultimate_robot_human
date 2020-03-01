using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject playerPrefab;
    int randomSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        Instantiate(playerPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);    
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            Instantiate(playerPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
        }
    }
}
