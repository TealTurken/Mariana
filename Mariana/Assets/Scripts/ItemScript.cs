using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemScript : MonoBehaviour
{
    private bool interactAllowed;
    public TextMeshProUGUI itemText;
    public GameObject minigameUI;
    private Inventory inventory;
    public GameObject itemButton;
    public bool terminalFixed = false;
    AudioSource audioSource;
    public AudioClip terminalSound;

    private void Start() 
    {
        itemText.gameObject.SetActive(false);
        minigameUI.SetActive(false);
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    
    private void Update()
    {
        if (interactAllowed && Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.tag == "Weapon") 
            {
                PickUp();
            }

            else if (gameObject.tag == "Key")
            {
                PickUp();
            }

            else if (gameObject.tag == "Tool")
            {
                PickUp();
            }

            else if (gameObject.tag == "Interactable")
            {
                Operate();
            }
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

            else if (gameObject.tag == "Tool")
            {
                itemText.SetText("Press 'E' to pick up");
            }

            else if (gameObject.tag == "Key")
            {
                itemText.SetText("Press 'E' to pick up");
            }

            else if (gameObject.tag == "Interactable")
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
            minigameUI.SetActive(false);
            interactAllowed = false;
        }
    }

    public async void PickUp()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false)
            {
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform, false);

                if (gameObject.tag == "Tool")
                {
                    inventory.hasTool = true;
                }

                Destroy (gameObject);
                print ("Item picked up");
                break;
            }
        }

        
    }

    private void Operate()
    {
        if (!inventory.hasTool)
        {
            itemText.SetText("The terminal appears to be broken...");
        }

        else if (inventory.hasTool && !terminalFixed)
        {
            minigameUI.SetActive(true);
            itemText.gameObject.SetActive(false);
        }



        PlaySound(terminalSound);
    }
}
