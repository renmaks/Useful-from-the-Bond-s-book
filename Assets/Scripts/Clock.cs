using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Tick());
    }

    IEnumerator Tick()
    {
        while(true)
        {
            print(System.DateTime.Now.ToString());
            yield return new WaitForSeconds(1);
        }
    }
}
