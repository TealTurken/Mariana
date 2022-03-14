using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemScript : MonoBehaviour
{
    private bool interactAllowed;
    public TextMeshProUGUI itemText;

    
    private void Start() 
    {
        itemText.gameObject.SetActive(false);
    }
    
    private void Update()
    {
        if (interactAllowed && Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.tag == "Weapon") PickUp();
            if (gameObject.tag == "Interactable") Operate();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            if (gameObject.tag == "Weapon")
            {
                itemText.SetText("Press 'E' to pick up");
            }
            if (gameObject.tag == "Interactable")
            {
                itemText.SetText("Press 'E' to use terminal");
            }
            itemText.gameObject.SetActive(true);
            interactAllowed = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            itemText.gameObject.SetActive(false);
            interactAllowed = false;
        }
    }

    private void PickUp()
    {
        print ("Item picked up");
        Destroy (gameObject);
    }

    private void Operate()
    {
        itemText.SetText("Working...\nFEATURE INCOMPLETE\n'to wait 3-5 seconds upon use to finish'");
    }
    
}
