using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEffect : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip shootSFX;
    [SerializeField] private AudioClip swingSFX;

    private bool isGrounded;
    private bool shootOnce;
    private GameObject grapplingGun;

    private void Awake()
    {
        grapplingGun = GameObject.Find("GrapplingGun");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GetComponent<AudioSource>().PlayOneShot(shootSFX);
            shootOnce = true;
        }

        if (grapplingGun.GetComponent<GrapplingGun>().onGround())
        {
            GetComponent<AudioSource>().Stop();
        }

        else if (shootOnce && !GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().PlayOneShot(swingSFX);
        }
            
    }
}

    
