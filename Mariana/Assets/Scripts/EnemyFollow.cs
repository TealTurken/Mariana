using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public float moveSpeed;
    public float stoppingDistance;
    float horizontal;
    float vertical;

    private Transform target;


    
    // Start is called before the first frame update
    void Start()
    {
       rigidbody2d = GetComponent<Rigidbody2D>();
       target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
    }

    // Update is called once per frame
    void Update()
    {
       Vector2 position = rigidbody2d.position;
       rigidbody2d.MovePosition(position); 
       
       if(Vector2.Distance(transform.position, target.position) > stoppingDistance)
       {
          transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
       } 
    }
}
