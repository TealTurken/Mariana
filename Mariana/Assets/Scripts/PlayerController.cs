using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // is being used for debugging. To use, write "textbox.SetText("your text here");"
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Vector2 movement;

    float moveSpeed; // actual movement speed applied to player character
    float sprintSpeed; // actual sprint speed
    public float normalSpeed = 4.0f;
    public float normalSprintSpeed = 7.0f;
    public float underwaterSpeed = 2.0f;
    public float underwaterSprintSpeed = 4.5f;
    float sceneSpeed; // retains scene movement speed when sprint speed is applied
    float horizontal;
    float vertical;
    Scene newScene;
    Scene activeScene;
   
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        #region Movement speed
        newScene = SceneManager.GetActiveScene();
        if (newScene.IsValid())
        {
            if (newScene != activeScene)
            {
                activeScene = newScene;
                if (activeScene.name == "Scene")
                {
                    moveSpeed = normalSpeed; // Normal move speed
                    sprintSpeed = normalSprintSpeed;
                }
                if (activeScene.name == "Water1")
                {
                    moveSpeed = underwaterSpeed; // underwater move speed
                    sprintSpeed = underwaterSprintSpeed;
                }
            }
        }
        #endregion
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
           sceneSpeed = moveSpeed;
           moveSpeed = sprintSpeed;
       }

       if (Input.GetKeyUp(KeyCode.LeftShift))
       {
           moveSpeed = sceneSpeed;
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

