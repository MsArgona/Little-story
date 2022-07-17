using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    new private ParticleSystem particleSystem;

    [SerializeField] private int maxStars;
    private int curStars = 0;

    private Animator animator;

    GameManager gameManager;
    Menu menu;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }
    private void Start()
    {
        particleSystem.Stop();

        if (curStars >= maxStars)
        {
            animator.SetTrigger("Open");
            particleSystem.Play();
        }       
    }
    public void GrabStar()
    {
        curStars++;
        if (curStars == maxStars)
        {
            animator.SetTrigger("Open");
            particleSystem.Play();
        }

        //ибо не на всех сценах есть эти объекты
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        menu = FindObjectOfType<Menu>().GetComponent<Menu>();
        menu.StarWasGrabded(curStars, maxStars);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && curStars >= maxStars)
        {
            gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
            gameManager.LoadNextLevel();
        }
    }
}
