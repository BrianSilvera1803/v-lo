﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BpmUpdate : MonoBehaviour
{
	/// <summary>
	/// Affiche le bpm
	/// </summary>
	public Text displayBpm;

	/// <summary>
	/// Lecteur permetant de lire les pulsion capturer par le Bitalino
	/// </summary>
	public BITalinoReader reader;

	/// <summary>
	/// Port d'entrée du Bitalino où l'on capture les pulsions
	/// </summary>
	public int channelRead = 0;
	
	/// <summary>
    /// Le nombre de battement de coeur sur 10 secondes
    /// </summary>
	public int bpm = 0;
	
	/// <summary>
    /// Force des pulsions du battement de coeur
    /// </summary>
	public double frequence = 0.0f;
	
	/// <summary>
    /// Chrono pour les 10 secondes de lecture des pulsions
    /// </summary>
	public float timer = 0.0f;

	/// <summary>
	/// Indique si nous somme sur un pic ou non lors de la lecture de pulsion
	/// </summary>
	public bool pic = true;
	
	/// <summary>
    /// Valeur qui indique losrqu'on a un pic lors de la lecture de pulsion
    /// </summary>
	public float seuil = 6.0f;

	/// <summary>
	/// Compte le nombre de pic obtenu lors de la lecture du rythme cardiage via BITalino en 10 secondes et fait un produit en croix pour obtenir le battement par minute (bpm)
	/// Ajoute a la liste des bmp pour le calcul du taux d'accident
	/// </summary>
	void Update()
    {
		if(reader.asStart){
			timer += Time.deltaTime;
			if ((int)(timer % 60) < 10) 
			{
				foreach (BITalinoFrame f in reader.getBuffer())
				{
					frequence += f.GetAnalogValue(channelRead);
				}

				frequence = frequence / reader.BufferSize; 
				if (pic && frequence > seuil)
				{
					bpm += 1;
					pic = false;
				}
				else if (!pic && frequence < seuil)
				{
					pic = true;
				}
			}
			else
			{
				displayBpm.text = (6* bpm).ToString()  + " bpm";
				probaAccident.bpm.Add(6* bpm);
				bpm = 0;
				timer = 0.0f;
				pic = true;
			}
			frequence = 0.0f;
		}
	}
}
