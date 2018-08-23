//Author: Brandan Line
//Purpose: Testing the scene to see how a weapon will interact with the player and enemy.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
public class PlayerAttributes : MonoBehaviour {

	// Test Scripts for the Sword Games score
	// Player has 5 health (room for change)
	// Reaching 0 health the player dies and sets a high score
	public int playerHealth;
	public GameObject player;
	public GameObject cameraHead;
	bool immune = false;

	public Text health;

	public float immuneTimer = 0f;

	private float invincible = 0f;

	//get score script
	public gameScoreController scoreScript;
	public FightCoordinator coordScript;

	// Use this for initialization
	void Start () 
	{
		//Set players health to maximum
		playerHealth = 5;
		health.text = "Health: " + playerHealth.ToString ();
		//Test show the players health initially
		Debug.Log (playerHealth);
	}

	// Update is called once per frame
	void Update () {
		
		health.text = "Health: " + playerHealth.ToString ();


		if (playerHealth == 0) 
		{
			Debug.Log ("Dead");
			coordScript.GameOver ();
			this.enabled = false;
		}

		player.transform.position = cameraHead.transform.position;

		if (invincible > 0.0f)
		{
			invincible -= Time.deltaTime;
		}
	}
		
	//Function player takes damage on contact with the enemy only 1 at a time
	public void PlayerTakeDamage()
	{
		if (invincible <= 0.0f) {
			playerHealth--;
			invincible = 1.0f;
		}
	}

	public void OnTriggerEnter(Collider collider){
		if (collider.tag == "EnemyWeapon" || collider.gameObject.transform.parent.tag == "EnemyWeapon") {
				PlayerTakeDamage ();	
			}

	}
}
