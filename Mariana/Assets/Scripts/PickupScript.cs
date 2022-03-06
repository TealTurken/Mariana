using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupScript : MonoBehaviour
{
    private bool pickupAllowed;
    public TextMeshProUGUI pickupText;

    
    private void Start() 
    {
        pickupText.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (pickupAllowed && Input.GetKeyDown(KeyCode.E))
           PickUp();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            pickupText.gameObject.SetActive(true);
            pickupAllowed = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            pickupText.gameObject.SetActive(false);
            pickupAllowed = false;
        }
    }

    private void PickUp()
    {
        print ("Item picked up");
        Destroy (gameObject);
    }
    
}
