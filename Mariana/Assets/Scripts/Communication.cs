using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Communication : MonoBehaviour
{
    private bool interactAllowed;
    public GameObject contactUI;
    public TextMeshProUGUI itemText;
    AudioSource audioSource;
    public AudioClip walkieSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        contactUI.gameObject.SetActive(false);
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
                Contact();
            }
        }
    }

     private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
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
            contactUI.SetActive(false);
            interactAllowed = false;
        }
    }

     private void Contact()
    {
        contactUI.SetActive(true);
        itemText.gameObject.SetActive(false);

        PlaySound(walkieSound);
    }
}
