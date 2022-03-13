using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
          transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
       } 
       
       Vector2 position = rigidbody2d.position;
       position.x = position.x + moveSpeed * horizontal * Time.deltaTime;
       position.y = position.y + moveSpeed * vertical * Time.deltaTime;
       
       rigidbody2d.MovePosition(position); 
    }
}
