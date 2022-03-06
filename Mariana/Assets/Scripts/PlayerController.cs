using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Vector2 movement;

    public float moveSpeed = 4.0f;
    float horizontal;
    float vertical;

    private bool running;
   
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();  

        moveSpeed = 4.0f;
        running = false;
    }

    // Update is called once per frame
    void Update()
    {
       horizontal = Input.GetAxis("Horizontal");
       vertical = Input.GetAxis("Vertical");

       Vector2 move = new Vector2(horizontal, vertical);
    
       if (Input.GetKeyDown(KeyCode.LeftShift))
       {
           running = true;
           moveSpeed = 7.0f;
       }

       if (Input.GetKeyUp(KeyCode.LeftShift))
       {
           running = false;
           moveSpeed = 4.0f;
       }
       
       if (Input.GetKey("escape"))
            {
                print ("Quit");
                Application.Quit();
            }
    }

    void FixedUpdate() 
    {
      Vector2 position = rigidbody2d.position;
       position.x = position.x + moveSpeed * horizontal * Time.deltaTime;
       position.y = position.y + moveSpeed * vertical * Time.deltaTime;
       
       rigidbody2d.MovePosition(position); 
    }

}

