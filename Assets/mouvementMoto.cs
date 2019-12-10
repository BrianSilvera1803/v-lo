﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouvementMoto : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float horizontalSpeed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Q))
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);


        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        float h = horizontalSpeed * Input.GetAxis("Mouse X");

        transform.Rotate(0, h, 0);
    }
}
