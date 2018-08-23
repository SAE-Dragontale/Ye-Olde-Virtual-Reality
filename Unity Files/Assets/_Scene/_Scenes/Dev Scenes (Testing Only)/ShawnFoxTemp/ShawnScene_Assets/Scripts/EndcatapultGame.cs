using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndcatapultGame : MonoBehaviour {

	public Trigger1 T1;
	public Trigger2 T2;
	public Trigger3 T3;
	public Trigger4 T4;
	public Trigger5 T5;
	public Trigger6 T6;

	public ETrigger1 ET1;
	public Etrigger2 ET2;
	public Etrigger3 ET3;
	public Etrigger4 ET4;
	public Etrigger5 ET5;
	public Etrigger6 ET6;

	public bool isHasPlayerWon;
	public bool isHasEnermyWon;

    public GenericUIWindowScript window;
    public GenericUIWindowScript losedow;

    void Awake()
	{
		isHasPlayerWon = false;
		isHasEnermyWon = false;
	}


    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		AllTrigger ();

    }

	void AllTrigger()
	{
		if(T1.isTrigger1 == true && T2.isTrigger2 == true && T3.isTrigger3 == true && T4.isTrigger4 == true && T5.isTrigger5 == true && T6.isTrigger6 == true)
		{
			isHasPlayerWon = true;
            window.Show();
            enabled = false;
		}else if (ET1.isETrigger1 == true && ET2.isETrigger2 == true && ET3.isETrigger3 == true && ET4.isETrigger4 == true && ET5.isETrigger5 == true && ET6.isETrigger6 == true)
		{
			isHasEnermyWon = true;
            losedow.Show();
            enabled = false;
		}

	}
	
    
}
