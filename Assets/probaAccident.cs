using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class probaAccident : MonoBehaviour
{
    public static Dictionary<string,float> value = new Dictionary<string,float>();
    public static Dictionary<string,float> coeff = new Dictionary<string,float>();

    public static float nb_course = 0;
    public static float distance = 0;
    public static float nb_regard = 10;
    public static List<double> bpm = new List<double>(); // bpm de Sven sur 8km a vélo a plat
    public static int maxbpm = 220; // Pour le division du bpm
    public static Dictionary<string, float> convert = new Dictionary<string, float>();

    public static GameObject start = null;

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

    public static Toggle currentToggle(ToggleGroup tg)
    {
        IEnumerator<Toggle> toggleEnum = tg.ActiveToggles().GetEnumerator();
        toggleEnum.MoveNext();
        return toggleEnum.Current;
    }

    public static void initInfo(ToggleGroup sexe, InputField age, Dropdown fp, Dropdown corp, ToggleGroup mc, Dropdown gravite_mc, ToggleGroup medoc, Dropdown gravite_medoc)
    {
        value.Add("Sexe",convert[currentToggle(sexe).name]);
        coeff.Add("Sexe",0.02f);

        maxbpm = maxbpm - int.Parse(age.text);
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
        coeff.Add("nbCourse",0.15f);

        value.Add("dist",distance/40000);
        coeff.Add("dist",0.14f);

        value.Add("nbRegard", nb_regard/20);
        coeff.Add("nbRegard", 0.06f);
        
        //value.Add("bpm",bpm/220 - int.Parse(age.text));
        //coeff.Add("bpm", 0.11f);
    }

    public static float calculProba()
    {
        value["nbCourse"] = nb_course/15;
        value["dist"] = distance/40000;
        value["nbRegard"] = nb_regard/20;
        //value["bpm"] = bpm/maxbpm;
        
        float res = 0.0f;
        foreach (string key in value.Keys)
        { 
            res += value[key] * coeff[key];
        }
        
        return res;
    }

}
