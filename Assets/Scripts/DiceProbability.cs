using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceProbability : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numDice = 2;
    public int numSides = 6;
    public bool checkToCalculate = false;
    public int maxIterations = 10000;
    public float width = 16;
    public float height = 9;

    [Header("Set Dynamically")]
    public int[] dice;
    public int[] rolls;

    void Awake()
    {
        Camera cam = Camera.main;
        cam.backgroundColor = Color.black;
        cam.orthographic = true;
        cam.orthographicSize = 5;
        cam.transform.position = new Vector3(8, 4.5f, -10);
    }

    void Update()
    {
        if (checkToCalculate)
        {
            StartCoroutine(CalculateRolls());
            checkToCalculate = false;
        }
    }

    void OnDrawGizmos()
    {
        float minVal = numDice;
        float maxVal = numDice * numSides;

        if (rolls==null || rolls.Length == 0 || rolls.Length != maxVal+1)
        {
            return;
        }

        float maxRolls = Mathf.Max(rolls);
        float heightMult = 1f / maxRolls;
        float widthMult = 1f / (maxVal - minVal);

        Gizmos.color = Color.white;
        Vector3 v0, v1 = Vector3.zero;
        for (int i = numDice; i <= maxVal; i++)
        {
            v0 = v1;
            v1.x = ((float)i - numDice) * width * widthMult;
            v1.y = ((float)rolls[i]) * height * heightMult;
            if (i!=numDice)
            {
                Gizmos.DrawLine(v0, v1);
            }
        }
    }

    public IEnumerator CalculateRolls()
    {
        int maxValue = numDice * numSides;
        rolls = new int[maxValue + 1];

        dice = new int[numDice];
        for (int i = 0; i < numDice; i++)
        {
            dice[i] = (i == 0) ? 0 : 1;
        }

        int iterations = 0;
        int sum = 0;

        while (sum != maxValue)
        {
            RecursivelyAddOne(0);

            sum = SumDice();
            rolls[sum]++;

            iterations++;
            if (iterations%maxIterations == 0)
            {
                yield return null;
            }
        }
        print("Calculation Done");

        string s = "";
        for (int i = numDice; i <= maxValue; i++)
        {
            s += i.ToString() + " " + rolls[i].ToString("N0") + "\n";
        }

        int totalRolls = 0;
        foreach (int i in rolls)
        {
            totalRolls += i;
        }
        s += "\nTotal Rolls: " + totalRolls.ToString("N0") + "\n";

        print(s);
    }

    public void RecursivelyAddOne(int ndx)
    {
        if (ndx == dice.Length) return;

        dice[ndx]++;
        if (dice[ndx]>numSides)
        {
            dice[ndx] = 1;
            RecursivelyAddOne(ndx + 1);
        }
        return;
    }

    public int SumDice()
    {
        int sum = 0;
        for (int i = 0; i < dice.Length; i++)
        {
            sum += dice[i];
        }
        return (sum);
    }

}
