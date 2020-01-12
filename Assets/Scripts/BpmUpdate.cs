using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BpmUpdate : MonoBehaviour
{
	public BITalinoReader reader;
	public int channelRead = 0;
	public int bpm = 0;
	public double frequence = 0.0f;
	public float timer = 0.0f;
	public bool pic = true;
	public float seuil = 6.0f;

	// Update is called once per frame
	void Update()
    {
		if(reader.asStart){
			timer += Time.deltaTime;
			if ((int)(timer % 60) < 10) 
			{
				foreach (BITalinoFrame f in reader.getBuffer())
				{
					frequence += f.GetAnalogValue(channelRead);
				}

				frequence = frequence / reader.BufferSize; 
				if (pic && frequence > seuil)
				{
					bpm += 1;
					pic = false;
				}
				else if (!pic && frequence < seuil)
				{
					pic = true;
				}
			}
			else
			{
				Debug.Log(6 * bpm);
				probaAccident.bpm.Add(6* bpm);
				bpm = 0;
				timer = 0.0f;
				pic = true;
			}
			frequence = 0.0f;
		}
	}
}
