using UnityEngine;
using System.Collections;

//Script for special zombie in Zombie Game
//GADA243 - Sp15 - T. Wyitt Carlile

public class SpecZom : ZomController //expands on the basic zombie
{

	public GameObject spit;		//var to attach shot
	public Transform spitSpawn;	//var to attach shot spawn location
	public float fireRate;		//var for fire rate
	private float nextFire;		//var for firing delay

	void FixedUpdate()
	{
		if (Time.time > nextFire)
		{	
			//zombie spits at a fixed rate of fire
			nextFire = Time.time + fireRate;
			Instantiate(spit, spitSpawn.position, spitSpawn.rotation);
		}
	}
}
