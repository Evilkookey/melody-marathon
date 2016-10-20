using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "TimerThing") 
		{
			col.SendMessage ("Slow");
		}
			
	}
	// Update is called once per frame
	void Update () {
	
	}
}
