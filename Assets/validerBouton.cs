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
    public GameObject player;

    // Start is called before the first frame update
    // Détecter si un clic se produit
    public void OnClick()
    {
        if (age.text != "") {
            display.text = age.text + " ans \n" + fp.options[fp.value].text + "\n" + corp.options[corp.value].text + "\n" + gravite_mc.options[gravite_mc.value].text + "\n" + gravite_medoc.options[gravite_medoc.value].text + "\n" + nb_course_init.options[nb_course_init.value].text + "\n" + distance_init.options[distance_init.value].text;
            probaAccident.initInfo(sexe, age, fp, corp, mc, gravite_mc, medoc, gravite_medoc, nb_course_init, distance_init);
            fiche.SetActive(false);
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
