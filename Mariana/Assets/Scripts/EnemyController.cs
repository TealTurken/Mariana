using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
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

     void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
       if(Vector2.Distance(transform.position, target.position) > stoppingDistance)
       {
           float xDifference = Math.Abs(transform.position.x - target.position.x);
           float yDifference = Math.Abs(transform.position.y - target.position.y);

           if (xDifference > yDifference)
           {
                Vector2 xTarget = new Vector2(target.position.x, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, xTarget, moveSpeed * Time.deltaTime);
           }
           else if (yDifference > xDifference)
           {
                Vector2 yTarget = new Vector2(transform.position.x, target.position.y);
                transform.position = Vector2.MoveTowards(transform.position, yTarget, moveSpeed * Time.deltaTime);
           }
       }
       
       Vector2 position = rigidbody2d.position;
       position.x = position.x + moveSpeed * horizontal * Time.deltaTime;
       position.y = position.y + moveSpeed * vertical * Time.deltaTime;
       
       rigidbody2d.MovePosition(position);
    }
}
