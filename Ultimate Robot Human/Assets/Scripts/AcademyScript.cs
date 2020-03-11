using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class AcademyScript : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    int randomSpawnPoint;

    public void Awake()
    {
        //Academy.Instance.OnEnvironmentReset += EnvironmentReset;
    }

    void EnvironmentReset()
    {
        // Reset Agent location to random local relative position
        //randomSpawnPoint = Random.Range(0, spawnPoints.Length);
        //this.transform.position = spawnPoints[randomSpawnPoint].position;

        //Agent.transform.localPosition = spawnPoints[randomSpawnPoint].position;

        // Reset FinishLine location to random local relative position

    }
}
