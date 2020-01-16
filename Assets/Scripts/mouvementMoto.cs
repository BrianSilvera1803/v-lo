using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouvementMoto : MonoBehaviour
{
    /// <summary>
    /// Vitesse de déplacement normal
    /// </summary>
    public float moveSpeed = 35f;
    
    /// <summary>
    /// Vitesse de déplacement pendant une accélération
    /// </summary>
    public float moveSpeedShift = 50f;
    
    /// <summary>
    /// Vitesse de rotation
    /// </summary>
    public float horizontalSpeed = 2.0f;

    /// <summary>
    /// Permet de déplacer le véhicule avec les touches:
    /// Z : déplacement vers l’avant
    /// S : arrière
    /// Q : gauche
    /// D : droite*$
    /// Souris/flèche directionnel : rotation horizontal
    /// Shift : accélération
    /// Et on peut appuyer sur échap pour quitter la simulation
    /// </summary>
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKey(KeyCode.Z))
            transform.Translate(Vector3.forward * moveSpeedShift * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * moveSpeedShift * Time.deltaTime);

        if (Input.GetKey(KeyCode.Q))
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKey(KeyCode.Q))
            transform.Translate(Vector3.left * moveSpeedShift * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * moveSpeedShift * Time.deltaTime);


        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        float h = horizontalSpeed * Input.GetAxis("Mouse X");

        if (Input.GetKey(KeyCode.LeftArrow))
            h = -horizontalSpeed;

        if (Input.GetKey(KeyCode.RightArrow))
            h = horizontalSpeed;

        transform.Rotate(0, h, 0);
    }
}
