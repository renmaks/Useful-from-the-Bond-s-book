using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZenosFollower : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject poi;
    public float u = 0.1f;
    public Vector3 p0, p1, p01;

    void FixedUpdate()
    {
        p0 = this.transform.position;
        p1 = poi.transform.position;

        p01 = (1 - u) * p0 + u * p1;

        this.transform.position = p01;
    }
}
