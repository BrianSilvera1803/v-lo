using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class validerBouton : MonoBehaviour
{
    public ToggleGroup sexe;
    public InputField age;
    public Dropdown fp;
    public Dropdown corp;
    public ToggleGroup mc;
    public Dropdown gravite_mc;
    public ToggleGroup medoc;
    public Dropdown gravite_medoc;

    public Dropdown nb_course_init;
    public Dropdown distance_init;

    public Text display;

    public GameObject fiche;
    public GameObject background;
    public GameObject clock;
    public GameObject bpm;
    public GameObject player;

    // Start is called before the first frame update
    // Détecter si un clic se produit
    public void OnClick()
    {
        if (age.text != "") {
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
            fiche.SetActive(false);
            background.SetActive(false);
            clock.SetActive(true);
            bpm.SetActive(true);
            player.AddComponent<mouvementMoto>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            display.text = "Veuillez rentrer au moins un âge";
        }
    }
}
