using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DoorScript : MonoBehaviour
{
    private bool enterAllowed;
    public TextMeshProUGUI doorText;
    // Start is called before the first frame update
    void Start()
    {
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
            SceneManager.LoadScene("Water1");
        }
    }    
}
