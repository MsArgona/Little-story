using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    bool isAlreadyGrabbed;

    DoorTeleport doorTeleport;

    AudioSource audioSource;
    SpriteRenderer sprite;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        doorTeleport = FindObjectOfType<DoorTeleport>().GetComponent<DoorTeleport>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isAlreadyGrabbed)
        {
            isAlreadyGrabbed = true;

            audioSource.Play();
            doorTeleport.GrabStar();

            sprite.color = new Color(0f, 0f, 0f, 0f);

            Destroy(gameObject, 1f);
        }
    }
}
