using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public Animator animator;
    Vector2 movement;
    GameObject flashlight;
    GameObject firePoint;

    public int maxHealth = 5;
    public int currentHealth;
    public HealthBar healthBar;
    public TextMeshProUGUI deadText;
    public GameObject gameOverUI;
    private bool isInvulnerable;
    float moveSpeed; // actual movement speed applied to player character
    float sprintSpeed; // actual sprint speed
    #region Level Movement Speed
    public float normalSpeed = 4.0f;
    public float normalSprintSpeed = 7.0f;
    public float underwaterSpeed = 2.0f;
    public float underwaterSprintSpeed = 4.5f;
    #endregion levelmovementspeed
    float sceneSpeed; // retains scene movement speed when sprint speed is applied
    float horizontal;
    float vertical;
    Scene newScene;
    Scene activeScene;
    [HideInInspector]
    public Vector3 travelPoint;
    [SerializeField]
    private float invulnerablityDurationSeconds = 3.0f;

    AudioSource audioSource;
    public AudioSource musicSource;
    public AudioClip dmgSound;
    public AudioClip defeatSound;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        flashlight = this.transform.GetChild(0).gameObject; // gets the flashlight on the player
        firePoint = this.transform.GetChild(1).gameObject;
        #region Movement speed
        newScene = SceneManager.GetActiveScene();
        if (newScene.IsValid())
        {
            if (newScene != activeScene) // updates movement speed between interior and exterior levels
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
                sceneSpeed = moveSpeed;
            }
        }

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        deadText.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);

        #endregion
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    void Update()
    {
        //DontDestroyOnLoad(this);
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        FaceMouse();

        #region Sprint
        if (Input.GetKeyDown(KeyCode.LeftShift))
       {
           moveSpeed = sprintSpeed;
       }

       if (Input.GetKeyUp(KeyCode.LeftShift))
       {
           moveSpeed = sceneSpeed;
       }
        #endregion

        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(1);
            PlaySound(dmgSound);
        }

    }

    void FixedUpdate() 
    {
      Vector2 position = rigidbody2d.position;
       position.x = position.x + moveSpeed * horizontal * Time.deltaTime;
       position.y = position.y + moveSpeed * vertical * Time.deltaTime;
       rigidbody2d.MovePosition(position);

        
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;
        else maxHealth--;
        PlaySound(dmgSound);       
     
        StartCoroutine(InvulnerabilityFrames());
        
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        
        if (currentHealth <= 0) // Player Death
        {
            currentHealth = 0;
            Destroy(gameObject);
            deadText.gameObject.SetActive(true);
            gameOverUI.gameObject.SetActive(true);

            musicSource.clip = (defeatSound);
            musicSource.Play();
            musicSource.loop = false;  
            
            Debug.Log("You are dead");
            return;
        }

    }    

    private IEnumerator InvulnerabilityFrames()
    {
        //Debug.Log("Player is invulnerable");
        isInvulnerable = true;

        yield return new WaitForSeconds(invulnerablityDurationSeconds);

        isInvulnerable = false;
        //Debug.Log("Player is no longer invulnerable");
    }
    
    void FaceMouse() // this makes the flashlight face the direction the mouse is in
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        flashlight.transform.up = -direction;
        firePoint.transform.up = direction;
    }
}

