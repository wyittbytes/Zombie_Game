using UnityEngine;
using System.Collections;

//Script to destroy bullets in the zombie game
//GADA243 - Sp15 - T. Wyitt Carlile

public class DestroyBullet: MonoBehaviour 
{
	public float lifetime;	//sets the lifetime ("range") of the bullets

	//function to destroy bullet at end of range
	void Start()
	{
		//bullet destroyed after lifetime expires
		Destroy (gameObject, lifetime);
	}

	//function to destroy bullet when hitting walls
	void OnTriggerEnter(Collider other)
	{
			
		//if bullet hits wall, destroy
		if(other.tag == "Wall")	
		{
			Destroy(gameObject);
		}
	}

}
