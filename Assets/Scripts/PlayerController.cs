using UnityEngine;
using System.Collections;

//Script for Player Object in Zombie Game
//GADA243 - Sp15 - T. Wyitt Carlile

[System.Serializable]	//separate section in inspector
public class Boundary
{
	//variables for boundary limits
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	public float moveSpeed = 1;
	public float turnSpeed = 90.0f; //var for rotation speed
	public Boundary boundary;	//var to instance boundary

	public GameObject shot;		//var to attach shot
	public Transform shotSpawn;	//var to attach shot spawn location
	public float fireRate;		//var for fire rate
	private float nextFire;		//var for firing delay

	public GameController gameController;	//variable linking to game controller

	private AudioSource shotAudio;		//audio for shot
	private AudioSource deadAudio;		//audio for dead
	private AudioSource carDoorAudio;	//audio for car door

	public GameObject splat;			//variable to attach splat
	public GameObject playerSplat;		//variable to attach player splat
	
	void Start()
	{	
		//finds game controller to connect for game over and scoring
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {	//if game controller exists
			//connects to game controller
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		
		//if game controller cannot be found prints error
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' script");
		}
		AudioSource[] audios = GetComponents<AudioSource> ();
		shotAudio = audios [0];	
		deadAudio = audios [1];			//sets audio tracks
		carDoorAudio = audios [2];

	}

	void FixedUpdate()
	{
		//Gets the rotation from the keyboard controls
		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector3 rotor = new Vector3 (0.0f,moveHorizontal,0.0f );

		rigidbody.drag = 100;				//stops bouncing when hitting walls
		rigidbody.freezeRotation = true;	//stops rotations when hitting walls

		//rotates player 
		transform.Rotate (rotor * turnSpeed * Time.deltaTime);
	

		if(Input.GetKey(KeyCode.W))	//moves player forward
		{
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.S))	//moves player backward
		{
			transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
		}

		//limits player movement to inside the boundary 
		rigidbody.position = new Vector3
			(
				Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
				0.0f,
				Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
				);
	}

	void Update()
	{
		//fires shots when mouse trigger activated if not on delay
		if (Input.GetButton ("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;	//sets next delay
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			shotAudio.Play ();
		}
	}

	public IEnumerator OnTriggerEnter(Collider other)
	{
		if(other.tag == "Spit")	//destroys player if hit by spitter
		{
			deadAudio.Play ();
			Instantiate (splat, transform.position, transform.rotation);
			Destroy(collider);	
			yield return new WaitForSeconds(0.5f);
			Destroy (gameObject);
			Instantiate (playerSplat, transform.position, transform.rotation);
			gameController.GameOver(false); //game over, player lost
		}

		if(other.tag == "Car")	//destroys play if escapes via car
		{
			carDoorAudio.Play ();
			Destroy(collider);	//prevents moving car before being destroyed
			yield return new WaitForSeconds(0.2f);
			Destroy (gameObject);
			gameController.GameOver(true);	//game over, player won
			gameController.AddScore (100);	
		}
		
	}
}
