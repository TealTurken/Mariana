using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Medkit : MonoBehaviour
{
    private bool interactAllowed;
    public TextMeshProUGUI itemText;
    AudioSource audioSource;
    public AudioClip healSound;
    private PlayerController Player;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Player = FindObjectOfType<PlayerController>();
    }

     public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        if (interactAllowed && Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.tag == "Interactable")
            {
                Heal();
            }
        }
    }

     void OnCollisionEnter2D(Collision2D other)
    {
        
        if(other.collider.tag == "Player")
        {
            if (gameObject.tag == "Interactable")
            {
                itemText.SetText("Press 'E' to use pickup");
            }

            itemText.gameObject.SetActive(true);
            interactAllowed = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            itemText.gameObject.SetActive(false);
            interactAllowed = false;
        }
    }

    void Heal()
    {
        itemText.gameObject.SetActive(false);
        PlaySound(healSound);
        Player.currentHealth = 5;
        Player.healthBar.SetHealth(Player.currentHealth);
        Destroy(gameObject);
    }
}
