using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform spawnPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            //Destroy(collision.gameObject);
            //StartCoroutine("Respawn", 1f);
            collision.transform.position = spawnPoint.position;
        }
    }

    //private IEnumerator Respawn(float spawnDelay)
    //{
    //    yield return new WaitForSeconds(spawnDelay);
    //    Debug.Log("Respawning");
    //    Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    //}
}
