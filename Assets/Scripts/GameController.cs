using UnityEngine;
using System.Collections;

//Script to spawn zombies et al. in Zombie Game
//GADA243 - Sp15 - T. Wyitt Carlile

public class GameController : MonoBehaviour 
{

	public GameObject zombie;		//var to attach asteroid
	public Vector3 spawnValues;		//var for spawning coordinates
	public int hazardCount;			//var to set # of asteroids
	public float spawnWait;			//var for spawning delay
	public float startWait;			//var for start delay
	public float waveWait;			//var for wave delay
	public GameObject special;		//var to attach enemy
	public int specialFrequency;	//var to set # of enemies (0-10) 0 = none, 10 = all
	private bool chance;			//var to hold if special

	public GUIText scoreText;		//var to attach score GUI
	public GUIText restartText;		//var to attach restart GUI
	public GUIText gameOverText;	//var to attach game over GUI
	public GUIText timerText;
	public int timer;

	public bool gameOver;			//var is game over
	private bool restart;			//var is game restart
	private int score;				//score counter
	private int zone = 4;			//four spawning zones

	public GameObject car;			//var to attach car
	public GameObject carSpawnA;	//var to attach car spawner A
	public GameObject carSpawnB;	//var to attach car spawner B
		
	void Start()
	{
		gameOver = false;		//game not over
		restart = false;		//game not restart
		restartText.text = "";	//restart blank
		gameOverText.text = "";	//game over blank
		score = 0;				//score is zero
		UpdateScore(); 			//sets score to zero
		StartCoroutine (SpawnWaves ());	//begins spawn waves
		chance = false;					//is not special
		InvokeRepeating ("Countdown", 1.0f, 1.0f);	//starts countdown
	}

	void Update()
	{
		if (restart)	//if game ready for restart
		{
			//hitting 'r' will reload level
			if(Input.GetKeyDown(KeyCode.R))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}

	}

	//function to spawn waves over time
	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds(startWait); //returns delay
		while(true)	//repeat until killed
		{
		

			for(int i = 0; i < hazardCount; i++)	//creates asteroids
			{
		
				zone = Random.Range (1,5);

				//determines if zombie spawn is special
				if(Random.Range (1,11) < specialFrequency)
				{
					chance = true;
				}
				else
				{
					chance = false;
				}

				//zone coordinates
				if (zone == 1)
				{
					Vector3 spawnPosition = new Vector3 
						(Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
					Spawner (chance, spawnPosition);
				}
				if (zone == 2)
				{
					Vector3 spawnPosition = new Vector3 
						(spawnValues.x, spawnValues.y, Random.Range(-spawnValues.z, spawnValues.z));
					Spawner (chance, spawnPosition);
				}
				if (zone == 3)
				{
					Vector3 spawnPosition = new Vector3 
						(Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, -spawnValues.z);
					Spawner (chance, spawnPosition);
				}
				if (zone == 4)
				{
					Vector3 spawnPosition = new Vector3 
						(-spawnValues.x, spawnValues.y, Random.Range(-spawnValues.z, spawnValues.z));
					Spawner (chance, spawnPosition);
				}
				yield return new WaitForSeconds(spawnWait);
			}

			yield return new WaitForSeconds(waveWait);

			//if game over, set for restart
			if (gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
				break;		//kills loop
			}
		}
	}

	//function to tally score (accessible to outside scripts)
	public void AddScore(int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	//function to update score GUI
	void UpdateScore()
	{
		scoreText.text = "Score: " + score; 
	}

	//function to end game (accessible to outside scripts)
	public void GameOver(bool win)
	{
		timer = 0;
		if(win)
		{
			gameOverText.text = "You Survived!";
			gameOver = true;
		}
		else
		{
			gameOverText.text = "You Are Dead!";
			gameOver = true;
		}
	}

	//function to spawn zombies
	void Spawner(bool chance, Vector3 spawnPosition)
	{
		if(chance)
		{
			Instantiate (special, spawnPosition, Quaternion.identity);	//special created
		}
		else
		{
			Instantiate (zombie, spawnPosition, Quaternion.identity);	//std zombie created
		}
	}

	void Countdown()
	{
		//timer until rescue car arrives
		timer--;
		if (timer > 0)
		{
			timerText.text = "Time Until Rescue: " + timer;
		}
		else if (timer == 0)
		{
			timerText.text = "";
			SpawnCar();	//calls car spawner
		}
		else
		{
			timerText.text = "";
			return;
		}
	}

	void SpawnCar()
	{	
		//Spawns car at one of two locations off to the left of the board

		if (Random.Range(0,2)==1)
		{
			Vector3 spawnPosA = new Vector3 (carSpawnA.transform.position.x, carSpawnA.transform.position.y, carSpawnA.transform.position.z);
			Instantiate (car, spawnPosA, Quaternion.identity);
		}
		else
		{
			Vector3 spawnPosB = new Vector3 (carSpawnB.transform.position.x, carSpawnB.transform.position.y, carSpawnB.transform.position.z);
			Instantiate (car, spawnPosB, Quaternion.identity);
		}
	}
}
