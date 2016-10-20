using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	GameObject player;
	public string playerName;

	// Use this for initialization
	void Start () 
	{

	}

	void OnTriggerEnter(Collider playerCol)
	{ 
		//Debug.Log (player.tag);
		if (playerCol.tag == "Attack") 
		{
			
			player = GameObject.Find (playerName);
			player.SendMessage ("AddScore");
			Destroy (gameObject);
			//Debug.Log (player.tag);
		}
	}

	// Update is called once per frame
	void Update () 
	{
	
	}


}
