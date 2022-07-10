using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclic : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float theta = 0;
    public bool showCosX = false;
    public bool showSinY = false;

    [Header("Set Dynamically")]
    public Vector3 pos;

    void Update()
    {
        float radians = Time.time * Mathf.PI;
        theta = Mathf.Round(radians * Mathf.Rad2Deg) % 360;
        pos = Vector3.zero;
        pos.x = Mathf.Cos(radians);
        pos.y = Mathf.Sin(radians);

        Vector3 tPos = Vector3.zero;
        if (showCosX) tPos.x = pos.x;
        if (showSinY) tPos.y = pos.y;

        transform.position = tPos;
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        int inc = 10;
        for (int i = 0; i < 360; i+= inc)
        {
            int i2 = i + inc;
            float c0 = Mathf.Cos(i * Mathf.Deg2Rad);
            float c1 = Mathf.Cos(i2 * Mathf.Deg2Rad);
            float s0 = Mathf.Sin(i * Mathf.Deg2Rad);
            float s1 = Mathf.Sin(i2 * Mathf.Deg2Rad);
            Vector3 vC0 = new Vector3(c0, -1f - (i / 360f), 0);
            Vector3 vC1 = new Vector3(c1, -1f - (i2 / 360f), 0);
            Vector3 vS0 = new Vector3(1f + (i / 360f), s0, 0);
            Vector3 vS1 = new Vector3(1f + (i2 / 360f), s1, 0);

            Gizmos.color = Color.HSVToRGB(i / 360f, 1, 1);
            Gizmos.DrawLine(vC0, vC1);
            Gizmos.DrawLine(vS0, vS1);
        }

        Gizmos.color = Color.HSVToRGB(theta / 360f, 1, 1);
        Vector3 cosPos = new Vector3(pos.x, -1f - (theta / 360f), 0);
        Gizmos.DrawSphere(cosPos, 0.05f);
        if (showCosX) Gizmos.DrawLine(cosPos, transform.position);

        Vector3 sinPos = new Vector3(1f + (theta / 360f), pos.y, 0);
        Gizmos.DrawSphere(sinPos, 0.05f);
        if (showSinY) Gizmos.DrawLine(sinPos, transform.position);
    }
}
