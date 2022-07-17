using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevelManager : MonoBehaviour
{
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private GameObject chars;
    [SerializeField] private GameObject shariki;

    private AudioSource audioSource;

    private bool isPartyStarted = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        shariki.SetActive(false);
        audioSource.Stop();
        chars.SetActive(false);
        UIPanel.SetActive(false);
    }

    public void FoodAreReady()
    {
        if (isPartyStarted)
            return;

        shariki.SetActive(true);
        isPartyStarted = true;
        audioSource.Play();
        chars.SetActive(true);
        UIPanel.SetActive(true);
    }
}
