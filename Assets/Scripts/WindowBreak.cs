using UnityEngine;
using System.Collections;

//Script to break window in Zombie Game
//GADA243 - Sp15 - T. Wyitt Carlile

public class WindowBreak : MonoBehaviour {

	public int limitBreak = 3;	//var for amount of hits before break
	private int count;			//var to count hits
	private bool hasPlayed;		//var has sound played?

	void Start () 
	{
		count = 0;	//count starts at zero
		hasPlayed = false;	//sound has not played
	}
	
	void Update()
	{
		//only destroy if limit is met
		if (count > limitBreak)
		{
			if(hasPlayed)	//allows sounds to play before destruction
			{
				if (!audio.isPlaying)
				{
					Destroy (gameObject);
				}
				return; //prevents sound from looping
			}
			audio.Play (); //play sound
			hasPlayed = true; //sound has played
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Zombie")
		{
			count++;	//adds to counter for zombie hits
		}
	} 

}
