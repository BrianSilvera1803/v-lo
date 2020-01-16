using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class probaAccident : MonoBehaviour
{
    /// <summary>
    /// Valeur attribué aux facteurs
    /// </summary>
    public static Dictionary<string,float> value = new Dictionary<string,float>();
    
    /// <summary>
    /// Pourcentage d'importance de chaque facteurs
    /// </summary>
    public static Dictionary<string,float> coeff = new Dictionary<string,float>();

    /// <summary>
    /// Le nombre de crouse effectué
    /// </summary>
    public static float nb_course = 0;
    
    /// <summary>
    /// La distance total parcouru
    /// </summary>
    public static float distance = 0;
    
    /// <summary>
    /// Le nombre regard sur le gps ou la montre
    /// </summary>
    public static float nb_regard = 0;

    /// <summary>
    /// Liste des bpm sur un échantillon de 10 secondes
    /// </summary>
    public static List<int> bpm = new List<int>();

    /// <summary>
    /// Le bpm max calculé par la différence entre la fréquence cardiaque maximale théorique de 220 et l'âge
    /// </summary>
    public static int bpmMAX = 0;
    
    /// <summary>
    /// Tableau de conversion des facteurs en valeur
    /// </summary>
    public static Dictionary<string, float> convert = new Dictionary<string, float>();

    /// <summary>
    /// Point de départ à chaque début de nouvelle mission
    /// </summary>
    public static GameObject start = null;

    /// <summary>
    /// Initialise le tableau de conversion
    /// </summary>
    void Start()
    {
        convert.Add("Homme", 0.7f);
        convert.Add("Femme", 1.0f);
        
        convert.Add("0-18", 0.1f);
        convert.Add("18-35", 0.2f);
        convert.Add("35-64", 0.5f);
        convert.Add("64-Plus", 1.0f);

        convert.Add("Inexistante", 1.0f);
        convert.Add("Faible", 0.7f);
        convert.Add("Intermédiaire", 0.3f);
        convert.Add("Intense", 0.1f);

        convert.Add("Mince", 0.55f);
        convert.Add("Normal", 0.18f);
        convert.Add("Importante", 1.0f);

        convert.Add("Card_faible", 0.5f);
        convert.Add("Card_fort", 1.0f);

        convert.Add("Medoc_faible", 0.5f);
        convert.Add("Medoc_fort", 1.0f);
    }

    /// <summary>
    /// Récupère le toggle choisi dans un ToggleGroup
    /// </summary>
    /// <param name="tg">un ToggleGroup</param>
    /// <returns>le toggle choisi</returns>
    public static Toggle currentToggle(ToggleGroup tg)
    {
        IEnumerator<Toggle> toggleEnum = tg.ActiveToggles().GetEnumerator();
        toggleEnum.MoveNext();
        return toggleEnum.Current;
    }

    /// <summary>
    /// Fait la moyenne de la liste
    /// </summary>
    /// <param name="list">liste de valeur pour obtenir une moyenne</param>
    /// <returns>moyenne de la liste</returns>
    public static int mean(List<int> list)
    {
        if (list.Count <= 0)
            return 60;
            int total = 0;
        foreach(int i in list){
            total += i;
        }
        return (total / list.Count);
    }

    /// <summary>
    /// Récupère les informations de la fiche pour les ajouter au liste de "value" et "coeff"
    /// </summary>
    /// <param name="sexe">Sexe de l'utilisateur</param>
    /// <param name="age">Âge de l'utilisateur</param>
    /// <param name="fp">Forme physique de l'utilisateur</param>
    /// <param name="corp">Corpulence de l'utilisateur</param>
    /// <param name="mc">Maladie cardiaque de l'utilisateur</param>
    /// <param name="gravite_mc">Gravité de la maladie cardiaque si a</param>
    /// <param name="medoc">Prise de médicament de l'utilisateur</param>
    /// <param name="gravite_medoc">Dose de médicament pris si a</param>
    public static void initInfo(ToggleGroup sexe, InputField age, Dropdown fp, Dropdown corp, ToggleGroup mc, Dropdown gravite_mc, ToggleGroup medoc, Dropdown gravite_medoc)
    {
        value.Add("Sexe",convert[currentToggle(sexe).name]);
        coeff.Add("Sexe",0.02f);

        if (int.Parse(age.text) >= 0 && int.Parse(age.text) <= 18)
            value.Add("Age",convert["0-18"]);
        else if (int.Parse(age.text) >= 18 && int.Parse(age.text) <= 35)
            value.Add("Age",convert["18-35"]);
        else if (int.Parse(age.text) >= 35 && int.Parse(age.text) <= 64)
            value.Add("Age",convert["35-64"]);
        else
            value.Add("Age",convert["64-Plus"]);
        coeff.Add("Age",0.05f);

        value.Add("FP",convert[fp.options[fp.value].text.Split(char.Parse(" "))[0]]);
        coeff.Add("FP",0.11f);

        value.Add("Corp",convert[corp.options[corp.value].text]);
        coeff.Add("Corp",0.10f);

        if(currentToggle(mc).name.CompareTo("Oui") == 0)
        {
            value.Add("MC",convert[gravite_mc.options[gravite_mc.value].text]);
            coeff.Add("MC",0.15f);
        }
        
        if(currentToggle(medoc).name.CompareTo("Oui") == 0)
        {
            value.Add("Medoc",convert[gravite_medoc.options[gravite_medoc.value].text]);
            coeff.Add("Medoc",0.11f);
        }

        value.Add("nbCourse", nb_course/15);
        coeff.Add("nbCourse",0.18f);

        value.Add("dist",distance/40000);
        coeff.Add("dist",0.17f);

        //value.Add("nbRegard", nb_regard/20);
        //coeff.Add("nbRegard", 0.06f);

        bpmMAX = 220 - int.Parse(age.text);
        value.Add("bpm", 60);
        coeff.Add("bpm", 0.11f);
    }

    /// <summary>
    /// Calcul le taux d'accident
    /// </summary>
    /// <returns>le taux d'accident</returns>
    public static float calculProba()
    {
        value["nbCourse"] = nb_course/15;
        value["dist"] = distance/40000;
        //value["nbRegard"] = nb_regard/20;
        value["bpm"] = mean(bpm) / bpmMAX;
        bpm.Clear();

        float res = 0.0f;
        foreach (string key in value.Keys)
        { 
            res += value[key] * coeff[key];
        }
        
        return res;
    }

}
