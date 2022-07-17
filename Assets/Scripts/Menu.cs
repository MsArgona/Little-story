using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("Pause Menu")]
    public static bool GameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;

    [Header("Stars Panel")]
    [SerializeField] private TextMeshProUGUI starCount;
    [SerializeField] private GameObject starsMenuUI;

    [Header("Sounds")]
    [SerializeField] private AudioClip clickButtonClip;

    [Header("Joke")]
    [SerializeField] private GameObject jokePanel;

    [Header("Happy bd UI")]
    [SerializeField] private GameObject HPPanel;
    [SerializeField] private bool isItFinalLevel;

    private AudioSource audioSource;
    private GameManager gameManager;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Time.timeScale = 1f;

        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

        if (isItFinalLevel)
        {
            HPPanel.SetActive(true);
            pauseMenuUI.SetActive(false);
            starsMenuUI.SetActive(false);
        }
        else
        {        
            if (jokePanel != null) jokePanel.SetActive(false);
            pauseMenuUI.SetActive(false);
            starsMenuUI.SetActive(true);
            starCount.text = "0";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);

        if (!isItFinalLevel) {
            starsMenuUI.SetActive(true);      
        }
        else
        {
            starsMenuUI.SetActive(false);
            HPPanel.SetActive(true);
        }       
    }

    private void Pause()
    {
        if (isItFinalLevel) {
            HPPanel.SetActive(false);
        }

        starsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StarWasGrabded(int curStars, int maxStars)
    {
        starCount.text = curStars + "/" + maxStars;
    }

    public void PlayCliclButtonSound()
    {
        audioSource.PlayOneShot(clickButtonClip);
    }

    public void PlayJoke()
    {
        jokePanel.SetActive(true);
    }
}
