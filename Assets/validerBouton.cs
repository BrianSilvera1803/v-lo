using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class validerBouton : MonoBehaviour
{
	public GameObject canvas;
	public static List<string> info;

	 void Start()
    {
        //this.info.add("test");
    }
    // Start is called before the first frame update
 // Détecter si un clic se produit
    public void OnClick()
    {
            // Sortie pour consoler le nom du GameObject cliqué et le message suivant. Vous pouvez remplacer ceci par vos propres actions lorsque vous cliquez sur le GameObject .
            Debug.Log ("objet de jeu cliqué avec le bouton droit de la souris!");
			canvas.SetActive(false);
    }
}
