using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouvementMoto : MonoBehaviour
{	
	public float moveSpeed = 10f;
	public float turnSpeed = 50f;
    // Start is called before the first frame update
  
    // Update is called once per frame
    void Update()
    {
		if(Input.GetKey(KeyCode.D))
			transform.Translate(10f,0,0);

		if(Input.GetKey(KeyCode.Z))
			transform.Translate(0,0,10f);

		if(Input.GetKey(KeyCode.Q))
			transform.Translate(-10f,0,0);

		if(Input.GetKey(KeyCode.S))
			transform.Translate(0,0,-10f);

    }
}
