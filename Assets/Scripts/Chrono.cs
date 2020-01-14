using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chrono : MonoBehaviour
{
    /// <summary>
    /// Affiche le temps
    /// </summary>
    public Text chrono;
    
    /// <summary>
    /// Représente le temps de la simulations
    /// </summary>
    public float chronoTime;

    /// <summary>
    /// Affiche le temps depuis le lancement de la simulation
    /// </summary>
    void Update()
    {
        chronoTime += Time.deltaTime;
        int temp = (int)chronoTime;
        int min = temp / 60;
        int sec = temp % 60;
        chrono.text = min.ToString() + ":" + sec.ToString();
    }
}
