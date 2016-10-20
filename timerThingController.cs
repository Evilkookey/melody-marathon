using UnityEngine;
using System.Collections;

public class timerThingController : MonoBehaviour 
{

	public float forwardSpeed;
	float verticalSpeed;
	int slow = 1;

	CharacterController characterController;

	// Use this for initialization
	void Start () 
	{
		characterController = GetComponent<CharacterController> ();
	}

	void Slow()
	{
		slow *= -1;
	}

	// Update is called once per frame
	void Update () 
	{
		if (characterController.isGrounded) 
		{

			verticalSpeed = 0;
			slow = 1;
		}
		else
		{
			verticalSpeed += slow * Physics.gravity.y * Time.deltaTime * 0.4f;
			if (verticalSpeed > -1.0f) 
			{
				verticalSpeed = -1.0f;
			}
		}

		//Debug.Log (slow + "   " + verticalSpeed + "");

		Vector3 speed = new Vector3 (-forwardSpeed, verticalSpeed, 0f);

		characterController.Move (speed * Time.deltaTime);
	}
}
