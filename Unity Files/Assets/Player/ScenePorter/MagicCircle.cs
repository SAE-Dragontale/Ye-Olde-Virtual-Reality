using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class MagicCircle : MonoBehaviour {

	[SerializeField]
	//GameObject magicCircle;
	GameObject circle;
	[SerializeField]
	bool _hasTeleported;
	bool _isTeleporting;
	string _destination;
	bool _inTavern;
	PostProcessingProfile _postProfile;
	void Awake()
	{
		if(Camera.main.GetComponent<PostProcessingBehaviour> ())
		{
			_postProfile = Camera.main.GetComponent<PostProcessingBehaviour> ().profile;
		}

		_hasTeleported = false;
		_isTeleporting = false;

	}




	// Use this for initialization
	void Start () {
		
	//	DontDestroyOnLoad (this.transform.root.gameObject);
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.F) && !_isTeleporting)
		{
			Debug.Log ("I should Teleport");
			RaycastHit hit;
			if(Physics.Raycast(transform.position,Vector3.down, out hit, 50))
			{


			}
			_isTeleporting = true;
			Debug.Log ("I hit" + hit.point);
			//		circle = Instantiate (magicCircle);
			//		circle.transform.position = new Vector3(hit.point.x, hit.point.y + .1f, hit.point.z);

			StartCoroutine ("RemoveWorld");

		}
	}

	
	IEnumerator RemoveWorld()
	{
		BeginParticles ();
		if(_postProfile != null)
		{
			StartCoroutine ("BloomScreen");
		}

		yield return new WaitForSeconds (1);


		Debug.Log ("Active scene was " + SceneManager.GetActiveScene ().name);
		SceneManager.LoadScene (_destination);


	}

	public void InTavern(bool tavernStatus)
	{
		_inTavern = tavernStatus;
	}

	public void SetDestination(string newDestination)
	{
		_destination = newDestination; 
	}



	void BeginParticles()
	{
		if(circle != null)
		{
			circle.GetComponent<ParticleSystem> ().Play ();
		}

	}

	public void StopParticles()
	{
		if(circle != null)
		{
			circle.GetComponent<ParticleSystem> ().Stop ();
		}
	}



	public bool ReturnTeleportStatus()
	{
		return _hasTeleported;
	}

	public void FinishedTeleporting()
	{
		_hasTeleported = false;
		_isTeleporting = false;
	//	Destroy (circle.gameObject);
	}


	IEnumerator BloomScreen()
	{
		Debug.Log ("I should bloom screen");
		var _bloom = _postProfile.bloom.settings;

		while (_bloom.bloom.intensity < 3)
			
		{
			_bloom.bloom.intensity += Time.deltaTime * 10;
			if(_bloom.bloom.intensity > 3)
			{
				_bloom.bloom.intensity = 3;

			}
			Debug.Log ("bloom intensity" + _bloom.bloom.intensity.ToString ());
			_postProfile.bloom.settings = _bloom;
			yield return null;
		}

	}

	public void TeleportToNext()
	{
		StartCoroutine ("RemoveWorld");
	}

}
