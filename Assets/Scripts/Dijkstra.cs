using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dijkstra : MonoBehaviour
{
    /// <summary>
    /// Liste des noeuds
    /// </summary>
    private List<GameObject> noeud_list = new List<GameObject>();

    /// <summary>
    /// Structure de tuple pour les paires de noeud lié par un arc
    /// </summary>
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

    /// <summary>
    /// Liste de paire de noeud lié par un arc
    /// </summary>
    public List<Tuple> t;
    
    /// <summary>
    /// Liste des arcs
    /// </summary>
    private List<GameObject> way_list = new List<GameObject>();
    
    /// <summary>
    /// Parent de tous les arcs
    /// </summary>
    public GameObject way;

    /// <summary>
    /// Liste des 2 noeud et de l'arc lié entre les deux
    /// </summary>
    public Dictionary<Tuple, GameObject> arc = new Dictionary<Tuple, GameObject>();

    /// <summary>
    /// Talbeau des distance entre le point de départ et les autres noeuds
    /// </summary>
    public Dictionary<GameObject, float> distance = new Dictionary<GameObject, float>();

    /// <summary>
    /// Tableau des prédécesseur de chaque noeuds
    /// </summary>
    public Dictionary<GameObject, GameObject> predecesseur = new Dictionary<GameObject, GameObject>();

    /// <summary>
    /// Parent des noeuds qui ne sont pas des noeud d'arrivé
    /// </summary>
    public GameObject point;

    /// <summary>
    /// Liste des noeuds d'arrivé
    /// </summary>
    private List<GameObject> goal_list = new List<GameObject>();

    /// <summary>
    /// Parent des noeuds d'arrivé
    /// </summary>
    public GameObject goal;

    /// <summary>
    /// Affichage du taux d'accident
    /// </summary>
    public Text display;

    /// <summary>
    ///  Initialise les sommets autres que sdeb à infini
    ///  La distance au sommet de départ sdeb est nulle
    /// </summary>
    /// <param name="sdeb"></param>
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

    /// <summary>
    /// On recherche un noeud de distance minimale (relié par l'arc de poids le plus faible) de sdeb parmi les noeuds situés hors de P. 
    /// On choisit un noeud de Q de distance minimale.
    /// </summary>
    /// <param name="Q">le complémentaire de P<param>
    /// <returns></returns>
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

    /// <summary>
    /// On met à jour les distances entre sdeb et s2 en se posant la question : vaut-il mieux passer par s1 ou pas ?
    /// </summary>
    /// <param name="s1">noeud de passage</param>
    /// <param name="s2">noeud dont la distance peut être mise à jour</param>
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

    /// <summary>
    /// Lance l'algorithme de Dijsktra : https://fr.wikipedia.org/wiki/Algorithme_de_Dijkstra
    /// </summary>
    /// <param name="sdeb">le point de départ</param>
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

    /// <summary>
    /// Cherche le plus court chemin entre sdeb et sfin
    /// </summary>
    /// <param name="sdeb">noeud de départ</param>
    /// <param name="sfin">noeud d'arrivé</param>
    /// <returns></returns>
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

    /// <summary>
    /// Initialise les liste de noeud et d'arc
    /// Lance l'algorithme de Dijkstra
    /// </summary>
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

    /// <summary>
    /// Retroune le prochain noeud d'arrivé par rapport au taux d'accident
    /// </summary>
    /// <param name="distanceMax">distance maximal entre le point de départ et d'arrivé</param>
    /// <returns></returns>
    public List<GameObject> nextGoal(float distanceMax)
    {
        List<GameObject> res = new List<GameObject>();
        foreach(GameObject goal in goal_list)
        {
            if (distance[goal] < distanceMax)
                res.Add(goal);
        }
        return res;
    }

    /// <summary>
    /// Lorsque le joueur entree dans le gameObject, on met à jour le nombre de course et la distance parcouru
    /// On calcul le nouveau taux d'accident et on choisit la prochaine mission par rapport à ce taux
    /// On cherche le plus court chemin entre le gameObject et la prochaine destination, et on l'affiche sur le gps.
    /// </summary>
    /// <param name="other">gameObject qui est entrée</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            probaAccident.nb_course = probaAccident.nb_course + 1.0f;

            if(probaAccident.start != null)
                probaAccident.distance = probaAccident.distance + (distance[probaAccident.start]*10); //Mise à l'échelle

            foreach (GameObject child in noeud_list)
            {
                child.SetActive(false);
            }

            foreach (Transform child in way.transform)
            {
                child.gameObject.SetActive(false);
            }

            goal_list.Remove(gameObject);
            probaAccident.start = gameObject;

            float prob = probaAccident.calculProba();

            display.text = prob.ToString(); 

            List<GameObject> next = new List<GameObject>();

            if (prob <= 0.4f)
            {
                next = nextGoal(110.0f);
            }
            else if(prob >= 0.4f && prob <= 0.6f)
            {
                next = nextGoal(60.0f);
            }
            else if (prob >= 0.6f && prob <= 0.8f)
            {
                next = nextGoal(40.0f);
            }
            else
            {
                next = nextGoal(20.0f);
            }

            int ind = Random.Range(0, next.Count);

            List<GameObject> l = PCC(gameObject, next[ind]);

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
