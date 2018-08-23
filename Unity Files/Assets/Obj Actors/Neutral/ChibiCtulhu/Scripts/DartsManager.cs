using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class DartsManager : MonoBehaviour {

	[SerializeField]
	MeshRenderer _speechBubble;
	[SerializeField]
	Texture[] _speechMats;
	int _quadrant;
	[SerializeField]
	Animator anim;
	[SerializeField]
	GameObject _bubble;
	AudioSource _audioPlayer;
	[SerializeField]
	AudioClip[] _clips;
	[SerializeField]
	PlayArea _playArea;
	[SerializeField]
	GameObject _scoreBubble;
	[SerializeField]
	TextMesh _timerMesh;
	[SerializeField]
	TextMesh _scoreMesh;
	[SerializeField]
	TextMesh _hiScoreMesh;
	bool _gamePlaying;
	int _score;
	int _hiScore;
	// Use this for initialization
	void Awake () {
		_gamePlaying = false;
		_score = 0;
		_hiScore = ReadHighScore();
		_hiScoreMesh.text = "Hi: " + _hiScore.ToString ();
		if(FindObjectOfType<WheelScoreBoard>())
		{
			FindObjectOfType<WheelScoreBoard> ().UpdateScoreboard (this);
		}
		_scoreMesh.text = "Score: 0";
		_audioPlayer = GetComponent<AudioSource> ();
		if(_playArea == null)
		{
			_playArea = FindObjectOfType<PlayArea> ();
		}
		_quadrant = -1;
	}
	
	// Update is called once per frame
	void Update () {
	
#if UNITY_EDITOR
		if(Input.GetKeyDown(KeyCode.G))
		{
			Debug.Log("Going to arena");
			FindObjectOfType<AdditiveSceneMethod.AdditiveSceneController>().CallToArena();
		}
		if(Input.GetKeyDown(KeyCode.H))
		{
			Debug.Log("Going to arena");
			FindObjectOfType<AdditiveSceneMethod.AdditiveSceneController>().CallToTavern();
		}
#endif


	}



	/// <summary>
	/// Randomizes a target for the player to hit and produces the appropriate speech bubble and audio sound.
	/// </summary>
	/// <returns>The bubble.</returns>
	public IEnumerator RandomizeBubble()
	{
		if(_bubble.activeInHierarchy)
		{
			_quadrant = -1;
			anim.SetTrigger ("Talk");
			_speechBubble.material.mainTexture = _speechMats [4];
			_audioPlayer.PlayOneShot (_clips [4]);
			yield return new WaitForSeconds (1.5f);
			_quadrant = Random.Range (0, 4);
			anim.SetTrigger ("Talk");
			_speechBubble.material.mainTexture = _speechMats [_quadrant];
			_audioPlayer.PlayOneShot(_clips[_quadrant]);
			yield return new WaitForSeconds (4.5f);
			StartCoroutine ("RandomizeBubble");
		}

	}


	/// <summary>
	/// Makes the octopus say a profanity and produces appropriate speech bubble.
	/// </summary>
	public IEnumerator Swear()
	{
		
		_quadrant = -1;
		if(!_bubble.activeInHierarchy)
		{
			StartCoroutine ("SwearWhenNotPlaying");
		}
		else
		{
			if(_playArea.ReturnInside())
			{
				StopCoroutine ("RandomizeBubble");
				anim.SetTrigger ("Talk");
				_speechBubble.material.mainTexture = _speechMats [5];
				_audioPlayer.PlayOneShot (_clips [5]);
				yield return new WaitForSeconds (2);
				StartCoroutine ("RandomizeBubble");
			}

		}

	}


	/// <summary>
	/// Lets octopus swear whilst the game is not active.
	/// </summary>
	/// <returns>The when not playing.</returns>
	public IEnumerator SwearWhenNotPlaying()
	{
		_bubble.SetActive (true);
		anim.SetTrigger ("Talk");
		_speechBubble.material.mainTexture = _speechMats [5];
		_audioPlayer.PlayOneShot (_clips [5]);
		yield return new WaitForSeconds (2);
		_bubble.SetActive (false);
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.GetComponent<Hand>() && col.gameObject.GetComponent<SphereCollider>().isTrigger)
		{
			return;

		}
		StartCoroutine ("Swear");

	}


	/// <summary>
	/// Begins the game, initializes various game variables.
	/// </summary>
	public void BeginGame()
	{
		StopAllCoroutines ();
		_bubble.SetActive (true);
		ActivateScoreBubble ();
		StartCoroutine ("GameTimer");
		StartCoroutine ("RandomizeBubble");
		_score = 0;
		_scoreMesh.text = "Score: 0";

		_hiScoreMesh.text = "Hi: " + _hiScore.ToString ();
		_gamePlaying = true;
	}


	/// <summary>
	/// Ends the game.
	/// </summary>
	public void EndGame()
	{
		_bubble.SetActive (false);
		StopAllCoroutines ();
		_gamePlaying = false;
		_scoreBubble.SetActive (false);
		_timerMesh.text = "0:00";
		_score = 0;
		_scoreMesh.text = "Score: 0";
		_quadrant = -1;//So player can't score outside of gaming area on the board
	}


	/// <summary>
	/// Returns the target quadrant.
	/// </summary>
	/// <returns>The target quadrant.</returns>
	public int ReturnTargetQuadrant()
	{
		return _quadrant;//Will be used by WheelQuadrant
	}


	/// <summary>
	/// Determines whether the player is inside the play area.
	/// </summary>
	/// <returns><c>true</c> if this instance is playing; otherwise, <c>false</c>.</returns>
	public bool IsPlaying()
	{
		return _playArea.ReturnInside ();
	}


	/// <summary>
	/// Increase the player's score if the game is active.
	/// </summary>
	public void PlayerWins()
	{
		
		if(_gamePlaying)
		{
			_audioPlayer.PlayOneShot (_clips [7]);
			//_audioPlayer.PlayOneShot (_clips [6]);
			_score++;
			_scoreMesh.text = "Score: " + _score.ToString ();
		}
#if UNITY_EDITOR
		Debug.Log ("You Scored!");
#endif

	}

	/// <summary>
	/// Players loses.
	/// </summary>
	public void PlayerLoses()
	{
		_audioPlayer.PlayOneShot (_clips [7]);
#if UNITY_EDITOR
		Debug.Log ("You lose!");
#endif

	}


	/// <summary>
	/// Starts the timer for the game, hi-score is updated if score surpasses it, writes hiscore to save file when game is finished.
	/// </summary>
	/// <returns>The timer.</returns>
	public IEnumerator GameTimer()
	{
		#if UNITY_EDITOR
		Debug.Log ("Game timer started");
		#endif
		float _timePrecise = 60;
		int _timeRounded = 60;
		_timerMesh.text = "0:" + _timeRounded.ToString ();
		while (_timePrecise > 1)
		{
			_timePrecise -= Time.deltaTime;
			_timeRounded =  Mathf.FloorToInt(_timePrecise);
			if(_timeRounded < 10)
			{
				_timerMesh.text = "0:0" + _timeRounded.ToString ();
			}
			else
			{
				_timerMesh.text = "0:" + _timeRounded.ToString ();
			}
			if(_score > _hiScore)
			{
				_hiScore = _score;
				_hiScoreMesh.text = "Hi: " + _hiScore.ToString ();
				WriteHighScore (_hiScore);
			}
			yield return null;
		}
		_timerMesh.text = "Finish!";
		if(_audioPlayer.isPlaying)
		{
			_audioPlayer.Stop ();
		}
		_audioPlayer.PlayOneShot (_clips [6]);
		_speechBubble.material.mainTexture = _speechMats [6];
		Invoke ("ResetGamePlaying", 2);
		StopAllCoroutines ();

	}

	void ResetGamePlaying()
	{
		Debug.Log ("Game can be replayed now");
		_bubble.SetActive (false);
		_gamePlaying = false;
	}


	/// <summary>
	/// Returns the play status of if a game is currently being played.
	/// </summary>
	/// <returns><c>true</c>, if play status was returned, <c>false</c> otherwise.</returns>
	public bool ReturnPlayStatus()
	{
		return _gamePlaying;
	}

	/// <summary>
	/// Reads the high score from the FourtuneHighScores.xml file
	/// </summary>
	/// <returns>The high score.</returns>
	int ReadHighScore() {
		int highScore = 0;
		if (!File.Exists (Application.persistentDataPath + "/FourtuneHighScores.xml")) {
			return highScore;
		}

		XmlReader reader = XmlReader.Create(Application.persistentDataPath + "/FourtuneHighScores.xml");
		while (reader.Read ()) { // Read cycles through each xml node.
			if (reader.NodeType == XmlNodeType.Text) { // If we reach an xml node of the text type
				highScore = int.Parse (reader.Value); // We store its value as our high score.
			}
		}

		return highScore; // return the returned highscore.
	}

	public void ActivateScoreBubble()
	{
		_scoreBubble.SetActive (true);
	}


	public int ReturnHighScore()
	{
		return _hiScore;
	}

	/// <summary>
	/// Writes the high score to FourtuneHighScores.xml file.
	/// </summary>
	/// <param name="score">Score.</param>
	void WriteHighScore(int score) {
		XmlDocument document = new XmlDocument ();
		XmlElement element = document.CreateElement ("HighScore");
		element.InnerText = score.ToString ();
		document.AppendChild (element);
		document.Save (Application.persistentDataPath + "/FourtuneHighScores.xml");

#if UNITY_EDITOR
		Debug.Log ("High score written");
#endif
	}
}
