//Author: Brandan Line
//Purpose: Control the start condition for swords as well as spawn enemies and the fighting 
// coordinator for swords.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Star condition When the player has picked up two objects (Sword and shield).

//Start action, spawn empty game object that has fight coordinator script

//Game over action, destroy the empty game object that has fight coordinator,
// present game over menu.

public class SwordSpawnController : MonoBehaviour {
	
	//create variables for the spawning system
	public GameObject coord;
	public GameObject coordObj;
	public GameObject coSpawn;

	//get script for player controller to access player health
	public PlayerAttributes playerSwordScript;
	public Hand rightHandScript;
	public Hand leftHandScript;
	
	// Update is called once per frame
	void Update () {

		coord.GetComponent<FightCoordinator> ().enabled = true;
		this.enabled = false;

		//if statement to check if the player is holding two object, use Roman's is holding from hand script?
		if (rightHandScript.IsHoldingItem () == true || leftHandScript.IsHoldingItem () == true)
		{
				
		}

	}



}
