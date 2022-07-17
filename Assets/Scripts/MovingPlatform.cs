using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int startPoint;
    [SerializeField] private Transform[] points;

    private int i;

    private void Start()
    {
        transform.position = points[startPoint].position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
                i = 0;
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }
}
