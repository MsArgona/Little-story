using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private int maxItems = 8;
    private int curItems = 0;

    private FinalLevelManager finalLevelManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Grabbable"))
        {
            curItems++;

            if (curItems == maxItems)
            {
                finalLevelManager = FindObjectOfType<FinalLevelManager>();
                finalLevelManager.FoodAreReady();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Grabbable"))
        {
            curItems--;

        }
    }
}
