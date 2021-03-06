using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedStateMachine : MonoBehaviour {

    enum DifferentGameStates { PlayersTurn,EnermysTurn,Win,Lose};

    public PlayerCatatuptBallTurnFinished playerProjectile;
    public EnermyProjectileTurn enermyProjectile;
    public FireCatapult fireCat;

    public bool isPlayerCanAttack;
    

    DifferentGameStates myStates;

    public bool IsenermyTurn;

    public AICatapult AIC;

    public bool isRunning;

    public bool isEnermyRunning;

	public EndcatapultGame endTheGame;
    // Use this for initialization
    void Start ()
    {
        isRunning = false;
        isEnermyRunning = false;

    fireCat = GameObject.FindGameObjectWithTag("FireCatapult").GetComponent<FireCatapult>();
        myStates = DifferentGameStates.PlayersTurn;
        AIC = GameObject.FindGameObjectWithTag("AIFireCatapult").GetComponent<AICatapult>();

        //playerProjectile.GetComponent<PlayerCatatuptBallTurnFinished>();
        // enermyProjectile.GetComponent<EnermyProjectileTurn>();
        playerProjectile.GetComponentInChildren<PlayerCatatuptBallTurnFinished> ();


    }
	
	// Update is called once per frame
	void Update ()
    {
        StateManager();

        //Debug.Log(myStates);

        
    }

    void StateManager()
    {
    
        if (fireCat.projectile.GetComponent<PlayerCatatuptBallTurnFinished>().Endplayersturn == true)
        {
           
          
            myStates = DifferentGameStates.EnermysTurn;
            IsenermyTurn = true;
            isPlayerCanAttack = false;

}

        if(fireCat.projectile.GetComponent<PlayerCatatuptBallTurnFinished>().Endplayersturn ==false)
        {
            myStates = DifferentGameStates.PlayersTurn;
            IsenermyTurn = false;
            isPlayerCanAttack = true;
        }
     
		if(endTheGame.isHasPlayerWon == true && endTheGame.isHasEnermyWon == false)
		{
			myStates = DifferentGameStates.Win;
		}

		if(endTheGame.isHasPlayerWon == false && endTheGame.isHasEnermyWon == true)
		{
			myStates = DifferentGameStates.Lose;
		}

    }
}
