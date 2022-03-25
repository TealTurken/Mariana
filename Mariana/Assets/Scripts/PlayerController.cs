using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    Vector2 movement;
    GameObject flashlight;

    int Health = 3;
    private bool isInvulnerable;
    float Oxygen = 100;
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
    [SerializeField]
    private float invulnerablityDurationSeconds = 3.0f;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        flashlight = this.transform.GetChild(0).gameObject; // gets the flashlight on the player
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

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
       
        #region 4 Directional Movement
        if (horizontal > 0.5 || horizontal < -0.5)
        {
            vertical = 0;
            if (horizontal > 0.5) flashlight.transform.rotation = Quaternion.Euler(0, 0, 90);
            if (horizontal < -0.5) flashlight.transform.rotation = Quaternion.Euler(0, 0, -90);
        }

        if (vertical > 0.5 || vertical < -0.5)
        {
            horizontal = 0;
            if (vertical > 0.5) flashlight.transform.rotation = Quaternion.Euler(0, 0, 180);
            if (vertical < -0.5) flashlight.transform.rotation = Quaternion.Euler(0, 0, 0);
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

    public void TakeDamage()
    {
        if (isInvulnerable) return;
        else Health = Health - 1;

        if (Health <= 0) // Player Death
        {
            Health = 0;
            Destroy(this);
            Debug.LogError("You are dead");
            return;
        }
        
        StartCoroutine(InvulnerabilityFrames());
    }    

    private IEnumerator InvulnerabilityFrames()
    {
        Debug.Log("Player is invulnerable");
        isInvulnerable = true;

        yield return new WaitForSeconds(invulnerablityDurationSeconds);

        isInvulnerable = false;
        Debug.Log("Player is no longer invulnerable");
    }

}

