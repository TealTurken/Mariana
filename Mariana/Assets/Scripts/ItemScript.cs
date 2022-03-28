using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemScript : MonoBehaviour
{
    private bool interactAllowed;
    public TextMeshProUGUI itemText;
    private Inventory inventory;
    public GameObject itemButton;

    private void Start() 
    {
        itemText.gameObject.SetActive(false);
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    
    private void Update()
    {
        if (interactAllowed && Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.tag == "Weapon") PickUp();
            if (gameObject.tag == "Key") Item();
            if (gameObject.tag == "Tool") Item();
            if (gameObject.tag == "Interactable") Operate();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            if (gameObject.tag == "Weapon, Tool, Key")
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

    private async void PickUp()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform, false);
                Destroy (gameObject);
                print ("Item picked up");
                break;
            }
        }
    }

    private void Item()
    {
        Destroy (gameObject);
        print ("Item picked up");
    }

    private void Operate()
    {
        itemText.SetText("Working...\nFEATURE INCOMPLETE\n'to wait 3-5 seconds upon use to finish'");
    }

    
    
}
