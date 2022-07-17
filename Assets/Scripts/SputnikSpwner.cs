using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SputnikSpwner : MonoBehaviour
{
    [SerializeField] private GameObject[] sputniks;
    [SerializeField] private float spawnTime = 10f;

    [SerializeField] private GameObject[] spawnPos;

    Queue<GameObject> sputniksQueue = new Queue<GameObject>();

    private void Start()
    {
        for (int i = 0; i < sputniks.Length; i++)
        {
            sputniksQueue.Enqueue(sputniks[i]);
        }

        InvokeRepeating("MoveSputnik", 0, spawnTime);
    }

    void MoveSputnik()
    {
        EnqueueSputniks();

        if (sputniksQueue.Count == 0)
            return;

        GameObject aSputnik = sputniksQueue.Dequeue();

        aSputnik.GetComponent<Sputnik>().IsMoving = true;
    }

    void EnqueueSputniks()
    {
        foreach (GameObject aSputnik in sputniks)
        {
            if ((aSputnik.transform.position.x < 0) && (!aSputnik.GetComponent<Sputnik>().IsMoving)){
                int i = Random.Range(0, spawnPos.Length - 1);
                aSputnik.GetComponent<Sputnik>().SetStartPos(spawnPos[i].transform);

                sputniksQueue.Enqueue(aSputnik);
            }
        } 
    }
}
