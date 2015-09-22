using UnityEngine;
using System.Collections;

//Script to destroy used explosions in Space Shooter
//GADA243 - Sp15 - T. Wyitt Carlile

public class DestroyByTime : MonoBehaviour 
{
	public float lifetime;	//sets the lifetime of the explosion

	//function to destroy explosion
	void Start()
	{
		//explosion destroyed after lifetime expires
		Destroy (gameObject, lifetime);
	}
}
