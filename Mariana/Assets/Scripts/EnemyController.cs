using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    public float moveSpeed;
    float horizontal;
    float vertical;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
       position.x = position.x + moveSpeed * horizontal * Time.deltaTime;
       position.y = position.y + moveSpeed * vertical * Time.deltaTime;
       
       rigidbody2d.MovePosition(position); 
    }
}
