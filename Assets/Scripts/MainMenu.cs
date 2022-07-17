using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private AudioClip clickButtonClip;
    [SerializeField] private GameObject startPanel;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Time.timeScale = 0f;
        mainMenuPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        mainMenuPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayCliclButtonSound()
    {
        audioSource.PlayOneShot(clickButtonClip);
    }
}
