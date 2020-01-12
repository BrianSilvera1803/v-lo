using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chrono : MonoBehaviour
{
    public Text chrono;
    public float chronoTime;

    // Update is called once per frame
    void Update()
    {
        chronoTime += Time.deltaTime;
        int temp = (int)chronoTime;
        int min = temp / 60;
        int sec = temp % 60;
        chrono.text = min.ToString() + ":" + sec.ToString();
    }
}
