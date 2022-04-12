using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorTrigger : MonoBehaviour
{
    private PlayerController Player;
    public TextMeshProUGUI doorText;
    bool isOpened = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (isOpened && Input.GetKeyDown(KeyCode.E))
        {
           transform.position = new Vector3(21, 66, 0); 
        }
    }  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            if (gameObject.tag == "Door")
            {
                isOpened = true;
                doorText.gameObject.SetActive(true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            isOpened = false;
            doorText.gameObject.SetActive(false);
        }
    }

      
}
