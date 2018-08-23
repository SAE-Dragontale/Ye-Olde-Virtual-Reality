//Author: Brandan Line
//Purpose: Save the players highscore in an xml file for the sword game. Along with 
// controlling the in game score text.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;
using System.IO;

public class gameScoreController: MonoBehaviour {
	//Text references for score
	public Text currentScoreText;
	public Text highScoreText;
	//Score variables
	public int currentScore = 0;
	public int highScore = 0;

	[SerializeField]
	public string gameName;

	// Use this for initialization
	void Start () {

		highScore = ReadHighScore ();
		highScoreText.text = "High Score: " + highScore.ToString ();
	}

	// Update is called once per frame
	void Update () {

	}

	//add score function, add the amount wanted when called
	public void AddScore(int score){
		//update score on kill
		currentScore += score;
		//update score text
		if (currentScoreText != null){
			currentScoreText.text = "Score: " + currentScore.ToString();
		}




		//if current score is more than the high score change the highscore
		if (currentScore > highScore){
			//update highscore
			highScore = currentScore;
			//update highscore text
			highScoreText.text = "High Score: " + highScore.ToString();
			//Write to the highscore file
			WriteHighScore ();
		}
	}

	//Read the file function to gather the highscore
	public int ReadHighScore(){
		int myHighScore = 0;

		//Read from xml file 
		if (gameName == "sword"){
			XmlReader reader = XmlReader.Create (Application.dataPath + "/StreamingAssets/SwordHighScores.xml");
			while (reader.Read ()) {
				if (reader.NodeType == XmlNodeType.Text) {
					myHighScore = int.Parse (reader.Value);
				}
			}
		}

		if (gameName == "wheel"){

			XmlReader reader = XmlReader.Create (Application.dataPath + "/StreamingAssets/WheelHighScores.xml");
			while (reader.Read ()) {
				if (reader.NodeType == XmlNodeType.Text) {
					myHighScore = int.Parse (reader.Value);
				}
			}
		}


		//return high score
		return myHighScore;
	}

	//Write to the file 
	public void WriteHighScore(){
		//create new Xml documet
		XmlDocument highScoreDoc = new XmlDocument ();
		//create a highscore element in the file
		XmlElement element = highScoreDoc.CreateElement ("HighScore1");

		element.InnerText = currentScore.ToString ();
		//append the the element (highscore) to the xml file
		highScoreDoc.AppendChild (element);

		//save the file in streaming assets path so that the score is held through build for swords and wheel game
		if (gameName == "sword"){
			highScoreDoc.Save (Application.dataPath + "/StreamingAssets/SwordHighScores.xml");
		}

		if (gameName == "wheel") {
			highScoreDoc.Save (Application.dataPath + "/StreamingAssets/WheelHighScores.xml");
		}

	}
}
