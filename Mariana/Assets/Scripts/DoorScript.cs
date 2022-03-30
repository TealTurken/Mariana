using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DoorScript : MonoBehaviour
{
    private bool enterAllowed;
    public TextMeshProUGUI doorText;
    private string destination;
    public Vector3 exitPoint; // variable for where the player appears in the scene they travel to
    private PlayerController Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Scene")
        {
            destination = "Water1";
        }
        if (currentScene.name == "Water1")
        {
            destination = "Scene";
        }
        doorText.gameObject.SetActive(false);
    }

    // Update is called once per frame

     private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            if (gameObject.tag == "Door")
            {
                enterAllowed = true;
                doorText.gameObject.SetActive(true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            enterAllowed = false;
            doorText.gameObject.SetActive(false);
        }
    }

      private void Update()
    {
        if (enterAllowed && Input.GetKeyDown(KeyCode.E))
        {
            Player.travelPoint = exitPoint;
            SceneManager.LoadScene(destination);
        }
    }    
}
