using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController e = collision.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.Stun();
        }
        
        Destroy(gameObject);
    }
}