using System;
using UnityEngine;

public class Pratiece : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loopexercise(0,100);
    }

    private void loopexercise(int v1, int v2)
    {
        for (int i = v1; i < v2; i++)
        {
            Debug.Log(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
