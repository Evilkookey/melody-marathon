//Bread_Doors();
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public Camera mainCam;

	public float forwardSpeed;
	public float jumpHeight;
	public int maxJumpCount;
	public GameObject timerThing;
	public float deathTimerMax;
	float deathTimer;

	public Color thisColor;

	public AudioSource song;

	int jumpCount;
	float verticalSpeed;

	public string jumpButton;
	public string powerupButton;
	public int TIME;
	public GameObject gameCanvas;
	public int P1Points;
	public GameObject pointText;

	bool powerUpbool;
	int powerupTimer;
	int storedPowerUp;

	public Text playerPowerup1;
	public Text playerPowerup2;

	bool alive;

	//Stuff what Greg Made//
	public string attackButton; 	//the button you attack with
	public GameObject attackBox;  	//the trigger on the character
	bool attacking;  				//true if youre attacking
	int counter;					//it counts the attack frames
	public int attackFrames;		//how many frames the attack is
	bool canAttack;					//checks if you ca attack

	public string slideButton;		//the button you slide with
	bool sliding = false;			// true if you are sliding
	bool canSlide = true;
	bool underObj = false;
	Vector3 slideHeight;
	Vector3 resetHeight;
	float moveAmount = 0.5f;

	CharacterController characterController;

	// Use this for initialization
	void Start () 
	{
		jumpCount = 0;
		alive = true;
		deathTimer = deathTimerMax;
		characterController = GetComponent<CharacterController> ();
		UpdateColour ();
		powerupTimer = TIME;


		slideHeight = new Vector3(gameObject.transform.localScale.x, (gameObject.transform.localScale.y / 2) , gameObject.transform.localScale.z);
		resetHeight = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y , gameObject.transform.localScale.z);

		//InvokeRepeating ("backgroundColour", 19.7f, 0.144f); //19.7f
	}

	void Dead()
	{
		alive = false;
		//Debug.Log ("dead");
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "PickUp" && alive == true) 
		{
			Destroy (other.gameObject);
			AddScore();
			song.volume = 1f;
		} 

		//MAXS CODE THO
		if (other.tag == "PowerUp1" && !powerUpbool)
		{
			Destroy(other.gameObject);
			powerUpbool = true;
			storedPowerUp = 1;
			playerPowerup1.gameObject.SetActive (true);
		}

		if (other.tag == "PowerUp2" && !powerUpbool)
		{
			Destroy(other.gameObject);
			powerUpbool = true;
			storedPowerUp = 2;
			playerPowerup2.gameObject.SetActive (true);
		}

		if (other.tag == "SlideCol") 
		{
			underObj = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "SlideCol") 
		{
			underObj = false;
			if (sliding == true && !(Input.GetKey (slideButton))) 
			{
				Slide (false);
			}
		}
	}

	void OnControllerColliderHit(ControllerColliderHit col)
	{
		
		if (col.transform.gameObject.tag == "Danger" || col.transform.gameObject.tag == "Enemy")// && alive == true) 
		{
			//Debug.Log (col.transform.gameObject.tag);
			Dead();
		}
	}

	void UpdateColour()
	{
		this.GetComponent<MeshRenderer> ().material.color = thisColor;
	}

	void PowerUp1()
	{
		powerupTimer--;

		if (powerupTimer < 1)
		{
			powerupTimer = TIME;
			storedPowerUp = 0;
			print("Count down finished");
			powerUpbool = false;
			CancelInvoke("PowerUp1");
		}

		//POWER UP CODE


	}
	void ResetPowerUp1()
	{

	}

	void PowerUp2()
	{
		powerupTimer--;

		if (powerupTimer < 1)
		{
			powerupTimer = TIME;
			storedPowerUp = 0;
			print("Count down finished");
			powerUpbool = false;
			CancelInvoke("PowerUp2");
		}
	}

	void ResetPowerUp2()
	{

	}

	void AddScore()
	{
		P1Points++;
	}

	void backgroundColour()
	{
		mainCam.backgroundColor = new Color( Random.value, Random.value, Random.value, 1.0f );
	}

	void Slide(bool slide)
	{
		if (slide == true) 
		{
			sliding = true;
			gameObject.transform.localScale = slideHeight;
			gameObject.transform.Translate (0, -moveAmount, 0);
		}

		if (slide == false) 
		{
			//Debug.Log ("dfnewufabeiuwl");
			sliding = false;
			canAttack = true;
			gameObject.transform.Translate (0, moveAmount, 0);
			gameObject.transform.localScale = resetHeight;
		}
	}
	// Update is called once per frame
	void Update ()
	{
		if (characterController.isGrounded) 
		{

			verticalSpeed = 0;
			jumpCount = 0;
			canAttack = true; //this---------------------
		}
		else
		{
			verticalSpeed += Physics.gravity.y * Time.deltaTime * 3;
		}
			
		if (Input.GetKeyDown (jumpButton) && jumpCount < maxJumpCount) 
		{
			verticalSpeed += jumpHeight;
			jumpCount++;
			//canSlide = false; //ASK TEAM
		}

		//MAXS CODE THO
		if(Input.GetKeyDown (powerupButton))
		{
			if (storedPowerUp == 1)
			{
				playerPowerup1.gameObject.SetActive (false);
				InvokeRepeating("PowerUp1", 1, 1);
				InvokeRepeating ("backgroundColour", 0, 0.144f); //19.7f
			}
			else if (storedPowerUp == 2)
			{
				playerPowerup2.gameObject.SetActive (false);
				InvokeRepeating("PowerUp2", 1, 1);
			}
		}

		//pointText.gameObject.GetComponent<Text> ().text = ;
		pointText.GetComponent<Text>().text = P1Points + "";


		//--------------------------------//

		if (Input.GetKeyDown (attackButton) && canAttack == true) 
		{
			attacking = true;
			canAttack = false;
			attackBox.SetActive (true);
			counter = 0;
		}

		if (attacking == true) 
		{
			verticalSpeed = 0;
			counter++;
			if (counter >= attackFrames) 
			{
				attackBox.SetActive (false);
				attacking = false;
			}
		}
			
		if (Input.GetKeyDown (slideButton) && (canSlide == true))
		{
			Slide (true);
		}
				
		if (Input.GetKeyUp (slideButton) && sliding == true && underObj == false) 
		{
			Slide (false);
		}


		//--------------------------------//

		float xDif = 0f;
		if (characterController.gameObject.transform.position.x < timerThing.transform.position.x + 0.5f) 
		{
			//Debug.Log ("true");
			//xDif = 0.5f;
			characterController.gameObject.transform.position.Set(timerThing.transform.position.x, characterController.gameObject.transform.position.y, 0f);
			xDif = 1.0f;
		} 
		else if(characterController.gameObject.transform.position.x > timerThing.transform.position.x - 0.5f)
		{
			//Debug.Log ("false");
			xDif = 2.0f;
		}

		//xDif = 1.0f;
		Vector3 speed = new Vector3 (-forwardSpeed *xDif, verticalSpeed, 0f);

		characterController.Move (speed * Time.deltaTime);

		//float xPos = characterController.gameObject.transform.position.x;

		if (alive == false) 
		{
			deathTimer -= 1 * Time.deltaTime;
			//Debug.Log (deathTimer + "");

			//this.GetComponent<MeshRenderer> ().material.color = new Color (0.0f, 1.0f, 0.0f, 0.5f);

			thisColor.a = 0.5f;
			//song.volume = 0.2f;
			//song.pitch = 1.5f;
			//forwardSpeed = 3f;
			UpdateColour ();

			Physics.IgnoreLayerCollision (8, 9);
			if (deathTimer < 0) 
			{
				deathTimer = deathTimerMax;
				alive = true;
				thisColor.a = 1.0f;
				UpdateColour ();

				Physics.IgnoreLayerCollision (8, 9, false);
			}
		}


	}
}



