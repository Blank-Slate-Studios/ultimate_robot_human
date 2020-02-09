using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFinish : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject finishPrefab;
    int randomSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        Instantiate(finishPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
