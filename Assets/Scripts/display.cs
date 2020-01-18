using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class display : MonoBehaviour
{
    /// <summary>
    /// Affichage de message
    /// </summary>
    public Text display_message;

    /// <summary>
    /// Temps affiché pendant la simulation
    /// </summary>
    public GameObject clock;

    /// <summary>
    /// GPS affiché pendant la simulation
    /// </summary>
    public GameObject gps;

    /// <summary>
    /// Affichage du nombre de cousrse
    /// </summary>
    public Text display_nb_course;

    /// <summary>
    /// Affichage de la distance total parcouru
    /// </summary>
    public Text display_distance_total;

    /// <summary>
    /// Affichage d'un message de prévention lorsque l'on atteint un taux trop élevé
    /// </summary>
    public GameObject message_panel;

    /// <summary>
    /// Affichage d'un message de prévention lorsque l'on atteint un taux trop élevé
    /// </summary>
    public Text message_prevent;
    
    /// <summary>
    /// Bouton pour choisir de faire une pause
    /// </summary>
    public GameObject pause;

    /// <summary>
    /// Bouton pour choisir de quitter la simulaiton
    /// </summary>
    public GameObject quit;

    /// <summary>
    /// Bouton pour choisir de continuer la simulation
    /// </summary>
    public GameObject continu;

    /// <summary>
    /// Bouton pour choisir de continuer la simulation
    /// </summary>
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        clock.SetActive(true);
        gps.SetActive(true);
    }

    void Update()
    {
        display_nb_course.text = probaAccident.nb_course.ToString() + "\n nb livraison";
        display_distance_total.text = probaAccident.distance.ToString() + "m";
        display_message.text = (probaAccident.calculProba() * 100).ToString() + "%";
    }
            

    public void MessageDePrevention(float proba)
    {
        if(proba >= 0.5f && proba <= 0.75f)
        {
            player.GetComponent<mouvementMoto>().enabled = false;
            message_panel.SetActive(true);
            quit.SetActive(true);
            continu.SetActive(true);
            pause.SetActive(true);
            message_prevent.text = "Le taux d'accident probable indique que vous avez plus d'une chance sur deux de faire un accident.\n Nous vous conseillons de vous faire une pause au de vous arréter.";
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(proba >= 0.75f && proba <= 1.0f)
        {
            player.GetComponent<mouvementMoto>().enabled = false;
            message_panel.SetActive(true);
            quit.SetActive(true);
            continu.SetActive(true);
            message_prevent.text = "Le taux d'accident probable indique que vous avez plus de 75% de chance de faire un accident. \n Nous vous conseillons fortement de vous arréter ";
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(proba >= 1.0f)
        {
            player.GetComponent<mouvementMoto>().enabled = false;
            message_panel.SetActive(true);
            quit.SetActive(true);
            message_prevent.text = "Tu es un danger public \n Arréte toi! c'est un ordre.";
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void onClick_Quit()
    {
        Application.Quit();
    }

    public void onClick_Continue()
    {
        player.GetComponent<mouvementMoto>().enabled = true;
        message_panel.SetActive(false);
        quit.SetActive(false);
        continu.SetActive(false);
        pause.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void onClick_Pause()
    {
        probaAccident.nb_course = 0;
        probaAccident.distance = 0;
        player.GetComponent<mouvementMoto>().enabled = true;
        message_panel.SetActive(false);
        quit.SetActive(false);
        continu.SetActive(false);
        pause.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
