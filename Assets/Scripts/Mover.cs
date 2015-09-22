using UnityEngine;
using System.Collections;

//Script to add object movement in Zombie Game
//GADA243 - Sp15 - T. Wyitt Carlile

public class Mover : MonoBehaviour 
{
	public float speed;		//variable to set bolt speed

	void Start()
	{
		//bolt set to move forward from source at given speed
		rigidbody.velocity = transform.forward * speed;
	}

}
