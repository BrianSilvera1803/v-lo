using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class validerBouton : MonoBehaviour
{
    public ToggleGroup sexe;
    public Dropdown age;
    public Dropdown fp;
    public Dropdown corp;
    public ToggleGroup mc;
    public Dropdown gravite_mc;
    public ToggleGroup medoc;
    public Dropdown gravite_medoc;

    public Dropdown nb_course_init;
    public Dropdown distance_init;

    public GameObject fiche;
    public GameObject player;

    // Start is called before the first frame update
    // Détecter si un clic se produit
    public void OnClick()
    {
        // Sortie pour consoler le nom du GameObject cliqué et le message suivant. Vous pouvez remplacer ceci par vos propres actions lorsque vous cliquez sur le GameObject .
        Debug.Log("objet de jeu cliqué avec le bouton droit de la souris!");
        probaAccident.initInfo(sexe, age, fp, corp, mc, gravite_mc, medoc, gravite_medoc, nb_course_init, distance_init);
        fiche.SetActive(false);
        player.AddComponent<mouvementMoto>();
    }
}
