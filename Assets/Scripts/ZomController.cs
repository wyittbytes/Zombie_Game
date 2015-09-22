using UnityEngine;
using System.Collections;

//Script for zombies in Zombie Game
//GADA243 - Sp15 - T. Wyitt Carlile

public class ZomController : MonoBehaviour 
{
	public static int numZombies;	//var for zombie count
	public bool kill;				//is player killed?
	public GameObject player;		//player container
	public float speed = 0.01f;		//zombie speed
	public float killDist = 0.2f;	//zombie reach

	public int scoreValue;					//variable to attach score value
	public GameController gameController;	//variable linking to game controller
	
	public GameObject splat;			//variable to attach splat
	public GameObject playerSplat;		//variable to attach player splat

	public void Start () 
	{
		player = GameObject.Find ("Player");	//finds player object
		numZombies++;							//zombie count
		//Debug.Log (numZombies);					//display zombie count

		//finds game controller to connect for game over and scoring
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)	//if game controller exists
		{
			//connects to game controller
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		
		//if game controller cannot be found prints error
		if (gameController == null)
		{
			Debug.Log("Cannot find 'GameController' script");
		}
	}

	public void Update () 
	{
		//if the game is over, destroy all the zombies
		if (gameController.gameOver && !audio.isPlaying)
		{
			Destroy(gameObject);

		}
		if (gameController.gameOver)
		{
			return;		//Takes care of missing player error on player destroy
		}

		rigidbody.drag = 100;				//prevents bouncing off walls
		rigidbody.freezeRotation = true;	//prevents rotating off walls

		//finds the players location in relation to the zombie
		Vector3 direction = 
			(player.transform.position - transform.position).normalized;
		float distance = 
			(player.transform.position - transform.position).magnitude;
		//moves zombie toward player
		Vector3 move = transform.position + (direction * speed);
		transform.position = move;
		//rotates zombie to face player (important for specials)
		transform.rotation = Quaternion.Slerp
			(transform.rotation, 
			 Quaternion.LookRotation(player.transform.position - transform.position), 
			 90.0f * Time.deltaTime);
		Vector3 rotor = new Vector3 (0.0f,move.y,0.0f );
		transform.Rotate (rotor * 90.0f * Time.deltaTime);

		if (distance <killDist)	//if within reach, kill
		{
			kill = true;
		}
		if (kill)	//if killed, destroy player
		{
			gameController.GameOver(false);		//calls to game over
			Destroy(player.gameObject);
			audio.Play ();
			Instantiate (playerSplat, transform.position, transform.rotation);
		}
	}

	//zombie counter display function
	public static void CountZombies()
	{
		Debug.Log (numZombies);
	}

	//zombie collider function
	public void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Bolt")	//if hit by gun, destroy zombie
		{
			numZombies--;
			Destroy (gameObject);
			Instantiate (splat, transform.position, transform.rotation);
			gameController.AddScore (scoreValue);
		}

		if (other.tag == "Car")	//if hit by car, destroy zombie
		{
			numZombies--;
			Destroy(gameObject);
			Instantiate (splat, transform.position, transform.rotation);
		}
	}

}
