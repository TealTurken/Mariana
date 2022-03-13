using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // is being used for debugging. To use, write "textbox.SetText("your text here");"
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Vector2 movement;

    public float moveSpeed = 4.0f;
    public TMP_Text textbox;
    float horizontal;
    float vertical;
   
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        moveSpeed = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
       horizontal = Input.GetAxis("Horizontal");
       vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
       
        #region 4 Directional Movement
        if (horizontal > 0.5 || horizontal < -0.5)
        {
            vertical = 0;
        }

        if (vertical > 0.5 || vertical < -0.5)
        {
            horizontal = 0;
        }
        #endregion

        #region Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift))
       {
           moveSpeed = 7.0f;
       }

       if (Input.GetKeyUp(KeyCode.LeftShift))
       {
           moveSpeed = 4.0f;
       }
        #endregion

        if (Input.GetKey("escape"))
            {
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #endif

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

