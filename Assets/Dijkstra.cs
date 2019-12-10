using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra : MonoBehaviour
{
    private List<GameObject> noeud_list = new List<GameObject>();

    [System.Serializable]
    public struct Tuple
    {
        public GameObject s1;
        public GameObject s2;
        public Tuple(GameObject s1,GameObject s2)
        {
            this.s1 = s1;
            this.s2 = s2;
        }
    }

    public List<Tuple> t;
    private List<GameObject> way_list = new List<GameObject>();
    public GameObject way;

    public Dictionary<Tuple, GameObject> arc = new Dictionary<Tuple, GameObject>();
    public Dictionary<GameObject, float> distance = new Dictionary<GameObject, float>();
    public Dictionary<GameObject, GameObject> predecesseur = new Dictionary<GameObject, GameObject>();

    public GameObject point;
    private List<GameObject> goal_list = new List<GameObject>();
    public GameObject goal;

    public void Init(GameObject sdeb)
    {
        foreach (GameObject go in noeud_list)
        {
            if (!distance.ContainsKey(go))
                distance.Add(go, Mathf.Infinity);
            else
                distance[go] = Mathf.Infinity;
        }
        distance[sdeb] = 0;
    }

    public GameObject TrouverMin(List<GameObject> Q)
    {
        float mini = Mathf.Infinity;
        GameObject sommet = Q[0];
        foreach (GameObject s in Q)
        {
            if (distance[s] < mini)
            {
                mini = distance[s];
                sommet = s;
            }
        }
        return sommet;
    }

    public void Maj_Distance(GameObject s1, GameObject s2)
    {
        if (arc.ContainsKey(new Tuple(s1,s2)))
        {
            if(distance[s2] > distance[s1] + arc[new Tuple(s1, s2)].transform.localScale.z)
            {
                distance[s2] = distance[s1] + arc[new Tuple(s1, s2)].transform.localScale.z;
                if (predecesseur.ContainsKey(s2)) { 
                    predecesseur[s2] = s1;
                }
                else
                {
                    predecesseur.Add(s2, s1);
                }
            }
        }
        else
        {
            if (distance[s2] > distance[s1] + arc[new Tuple(s2, s1)].transform.localScale.z)
            {
                distance[s2] = distance[s1] + arc[new Tuple(s2, s1)].transform.localScale.z;
                if (predecesseur.ContainsKey(s2))
                {
                    predecesseur[s2] = s1;
                }
                else
                {
                    predecesseur.Add(s2, s1);
                }
            }
        }
    }

    public void Dijkstra_run(GameObject sdeb)
    {
        Init(sdeb);
        List<GameObject> Q = new List<GameObject>(noeud_list);
        while(Q.Count != 0)
        {
            GameObject s1 = TrouverMin(Q);
            Q.Remove(s1);
            foreach(GameObject s2 in noeud_list)
            {
                if(arc.ContainsKey(new Tuple(s1, s2)) || arc.ContainsKey(new Tuple(s2, s1)))
                {
                    Maj_Distance(s1, s2);
                }
            }
        }
    }

    public List<GameObject> PCC(GameObject sdeb,GameObject sfin)
    {
        List<GameObject> chemin = new List<GameObject>();
        GameObject s = sfin;
        while(s != sdeb)
        {
            chemin.Add(s);
            s = predecesseur[s];
        }
        chemin.Add(sdeb);
        return chemin;
    }
    void Start()
    {
        foreach(Transform child in goal.transform)
        {
            goal_list.Add(child.gameObject);
            noeud_list.Add(child.gameObject);

        }

        foreach (Transform child in point.transform)
        {
            noeud_list.Add(child.gameObject);
        }

        foreach(Transform child in way.transform)
        {
            way_list.Add(child.gameObject);
        }

        for (int i = 0; i < t.Count; i++)
        {
            arc.Add(t[i], way_list[i]);
        }

        Dijkstra_run(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach (GameObject child in noeud_list)
            {
                child.SetActive(false);
            }

            foreach (Transform child in way.transform)
            {
                child.gameObject.SetActive(false);
            }
            goal_list.Remove(gameObject);

            probaAccident.nb_course = probaAccident.nb_course + 1;
            //probaAccident.distance = probaAccident.distance + distance[goal] ;
            
            int ind = Random.Range(0, goal_list.Count);
            List<GameObject> l = PCC(gameObject, goal_list[ind]);
            l.Reverse();

            foreach (GameObject e in l)
            {
                if(!e.CompareTag("Goal"))
                    e.SetActive(true);
            }
            l[l.Count - 1].SetActive(true);

            for (int i = 0; i < l.Count - 1; i++)
            {
                if (arc.ContainsKey(new Tuple(l[i], l[i + 1])))
                    arc[new Tuple(l[i], l[i + 1])].SetActive(true);
                else
                    arc[new Tuple(l[i + 1], l[i])].SetActive(true);
            }
            gameObject.SetActive(false);
        }
    }
}
