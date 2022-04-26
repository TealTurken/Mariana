using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip laserSound;
    public Transform firePoint;
    public GameObject laserPrefab;

    public float laserForce = 20f;
    [SerializeField]
    private float CooldownTime = 5f;
    private bool isOnCooldown = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            if (!isOnCooldown)
            {
                Shoot();
                StartCoroutine(Cooldown());
            }
        }
    }

    void Shoot ()
    {
        GameObject laser = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = laser.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * laserForce, ForceMode2D.Impulse);
        PlaySound(laserSound);
    }

    private IEnumerator Cooldown()
    {
        isOnCooldown = true;

        yield return new WaitForSeconds(CooldownTime);

        isOnCooldown = false;
    }
}
