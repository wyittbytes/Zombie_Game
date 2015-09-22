using UnityEngine;
using System.Collections;

//Script to utilize Boundary in Zombie Game
//GADA243 - Sp15 - T. Wyitt Carlile

public class DestroyByBoundary : MonoBehaviour 
{
	//function to destroy objects leaving the boundary
	void OnTriggerExit(Collider other)
	{
		Destroy (other.gameObject);
	}

}
