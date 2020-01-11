using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BpmUpdate : MonoBehaviour
{
	public BITalinoReader reader;
	public int channelRead = 0;

    // Update is called once per frame
    void Update()
    {
		if(reader.asStart){
			foreach(BITalinoFrame f in reader.getBuffer())
			{
				probaAccident.bpm.Add(f.GetAnalogValue(channelRead));
				Debug.Log(f.GetAnalogValue(channelRead));
			}
		}
	}
}
