//Author: Brandan Line
//Purpose: Testing the scene to see how a weapon will interact with the player and enemy.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class PlayerTestBrandan : MonoBehaviour {

	// Test Scripts for the Sword Games score
	// Player has 5 health (room for change)
	// Reaching 0 health the player dies and sets a high score
	public int PlayerTestHealth;
	public GameObject player;

	//get score script
	public gameScoreController scoreScript;


	// Use this for initialization
	void Start () 
	{
		//Set players health to maximum
		PlayerTestHealth = 5;
		//Test show the players health initially
		Debug.Log (PlayerTestHealth);
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerTestHealth == 0) 
		{
			SwordTestPlayerDie ();
		}
	}


	//Function player dies and calls the save score function
	public void SwordTestPlayerDie()
	{
		// Call the score function from the score script
	}

	//Function player takes damage on contact with the enemy only 1 at a time
	public void SwordTestTakeDamage()
	{
		PlayerTestHealth = PlayerTestHealth - 1;
	}
}
