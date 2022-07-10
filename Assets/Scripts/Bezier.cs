using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float timeDuration = 1;
    public Transform c0, c1, c2, c3;
    public bool checkToStart = false;

    [Header("Set Dynamically")]
    public float u;
    public Vector3 p0123;
    public bool moving = false;
    public float timeStart;

    void Update()
    {
        if (checkToStart)
        {
            checkToStart = false;
            moving = true;
            timeStart = Time.time;
        }

        if (moving)
        {
            u = (Time.time - timeStart) / timeDuration;
            if (u>=1)
            {
                u=1;
                moving = false;
            }

            Vector3 p01, p12, p23, p012, p123;

            p01 = (1 - u) * c0.position + u * c1.position;
            p12 = (1 - u) * c1.position + u * c2.position;
            p23 = (1 - u) * c2.position + u * c3.position;

            p012 = (1 - u) * p01 + u * p12;
            p123 = (1 - u) * p12 + u * p23;

            p0123 = (1 - u) * p012 + u * p123;

            transform.position = p0123;
        }
    }
}
