using UnityEngine;
using System.Collections;

//Script to move car in Zombie Game
//GADA243 - Sp15 - T. Wyitt Carlile

public class CarMover : MonoBehaviour 
	{
		public float speed;		//variable to set speed
		
		void Start()
		{
			//car moves from left to right 
			rigidbody.velocity = transform.right * speed;
		}
		
	}