using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpike : MonoBehaviour
{
    Rigidbody2D rigidbody; 
    // Start is called before the first frame update
    void Start()
    {
        rigidbody.GetComponent<Rigidbody2D> ();

    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals ("AI"))
        rigidbody.isKinematic = false;
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.name.Equals ("AI"))
            Debug.Log("Spike Hits!");
    }
}
