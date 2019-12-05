using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class probaAccident : MonoBehaviour
{ 
    public static List<string> info = new List<string>();
    public static int nb_course;
    public static int distance;
    
    public static Toggle currentToggle(ToggleGroup tg)
    {
        IEnumerator<Toggle> toggleEnum = tg.ActiveToggles().GetEnumerator();
        toggleEnum.MoveNext();
        return toggleEnum.Current;
    }

    public static void display_info()
    {
        foreach(string s in info)
        {
            Debug.Log(s);
        }
    }

    public static void initInfo(ToggleGroup sexe, Dropdown age, Dropdown fp, Dropdown corp, ToggleGroup mc, Dropdown gravite_mc, ToggleGroup medoc, Dropdown gravite_medoc, Dropdown nb_course_init, Dropdown distance_init)
    {
        info.Add(currentToggle(sexe).name);
        info.Add(age.options[age.value].text);
        info.Add(fp.options[fp.value].text);
        info.Add(corp.options[corp.value].text);
        info.Add(currentToggle(mc).name);
        info.Add(gravite_mc.options[gravite_mc.value].text);
        info.Add(gravite_medoc.options[gravite_medoc.value].text);
        info.Add(nb_course_init.options[nb_course_init.value].text);
        info.Add(distance_init.options[distance_init.value].text);

        display_info();
    }



}
