using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class validerBouton : MonoBehaviour
{

    /// <summary>
    /// Lecteur de rythme cardique
    /// </summary>
	public GameObject BITalino;

    /// <summary>
    /// Entré du nom de port bluetooth pour le lecteur de rythme cardiaque
    /// </summary>
	public InputField COM;

    /// <summary>
    /// Choix du sexe de l'utilisateur
    /// </summary>
    public ToggleGroup sexe;
    
    /// <summary>
    /// Choix de l'âge de l'utilisateur
    /// </summary>
    public InputField age;

    /// <summary>
    /// Choix de la orme physique de l'utilisateur
    /// </summary>
    public Dropdown fp;

    /// <summary>
    /// Choix de la corpulence de l'utilisateur
    /// </summary>
    public Dropdown corp;

    /// <summary>
    /// Choix de la maladie cardiaque de l'utilisateur
    /// </summary>
    public ToggleGroup mc;

    /// <summary>
    /// Choix de la gravité de la maladie cardiaque si a
    /// </summary>
    public Dropdown gravite_mc;

    /// <summary>
    /// Choix de la prise de médicament de l'utilisateur
    /// </summary>
    public ToggleGroup medoc;

    /// <summary>
    /// Choix de la dose de médicament pris si a
    /// </summary>
    public Dropdown gravite_medoc;

    /// <summary>
    /// Choix de nombre de course déja effectuer
    /// </summary>
    public Dropdown nb_course_init;
    
    /// <summary>
    /// Choix de la distance déja parcouru
    /// </summary>
    public Dropdown distance_init;

    /// <summary>
    /// Affichage de message
    /// </summary>
    public Text display;

    /// <summary>
    /// Fiche de l'utilisateur
    /// </summary>
    public GameObject fiche;
    
    /// <summary>
    /// Image de fond 
    /// </summary>
    public GameObject background;
    
    /// <summary>
    /// Temps affiché pendant la simulation
    /// </summary>
    public GameObject clock;
    
    /// <summary>
    /// Mise à jour du bpm
    /// </summary>
    public GameObject bpm;
    
    /// <summary>
    /// Avatar de l'utilisateur
    /// </summary>
    public GameObject player;

    /// <summary>
    /// Quand on clique sur le bouton "valider" de la fiche, on entre toute les valeurs sur la fiche dans probaAccident
    /// On désactive la fiche, l'image de fond et on active l'heure, la mise a jour du bpm et le joueur
    /// </summary>
    public void OnClick()
    {
        if (age.text != "" && COM.text != "") {
            probaAccident.nb_course = float.Parse(nb_course_init.options[nb_course_init.value].text);
            switch (distance_init.value)
            {
                case 0:
                    probaAccident.distance = Random.Range(0,2500);
                    break;
                case 1:
                    probaAccident.distance = Random.Range(2500, 5000);
                    break;
                case 2:
                    probaAccident.distance = Random.Range(5000, 7500);
                    break;
                case 3:
                    probaAccident.distance = Random.Range(7500, 10000);
                    break;
                case 4:
                    probaAccident.distance = 10000;
                    break;
            }
            
            
            
            probaAccident.initInfo(sexe, age, fp, corp, mc, gravite_mc, medoc, gravite_medoc);
			BITalino.GetComponent<BITalinoSerialPort>().portName = COM.text;
            fiche.SetActive(false);
            background.SetActive(false);
			BITalino.SetActive(true);
            clock.SetActive(true);
            bpm.SetActive(true);
            player.AddComponent<mouvementMoto>();
			display.text = "";
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            display.text = "Veuillez rentrer au moins un âge et un nom de port";
        }
    }
}
