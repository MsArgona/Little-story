using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokeTrigger : MonoBehaviour
{
    private AudioSource audioSource;
    private Menu menu;

    private bool isAlreadyTriggered;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {      
        if (collision.CompareTag("Player") && !isAlreadyTriggered)
        {
            audioSource.Play();
            isAlreadyTriggered = true;
            menu = FindObjectOfType<Menu>().GetComponent<Menu>();
            menu.PlayJoke();
        }
    }
}
