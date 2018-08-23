using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctoSpawner : MonoBehaviour {

	[SerializeField]
	GameObject _prefab;
	[SerializeField]
	GameObject[] _spawnPoints;
	// Use this for initialization
	void Start () {
		CreateOctopus ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}




	public void CreateOctopus()
	{
		int randSpawn = Random.Range (0, 6);
		GameObject octopus = Instantiate (_prefab, (_spawnPoints[randSpawn].transform.position + new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(0.0f, 2.0f), Random.Range(-2.0f, 2.0f))), Quaternion.Euler(0, 0, 90));
	//	octopus.transform.LookAt (FindObjectOfType<MagicCircle> ().gameObject.transform);
		octopus.transform.rotation = Quaternion.Euler (0, octopus.transform.rotation.eulerAngles.y + 180, 90);
		octopus.transform.SetParent (this.gameObject.transform);
		Debug.Log ("randspqawn is " + randSpawn + "position is " + _spawnPoints [randSpawn].transform.position);
//		octopus.transform.position = _spawnPoints [randSpawn].transform.position;
//		octopus.GetComponentInChildren<ChibiCthulhuInteraction> ().transform.localPosition = new Vector3 (0, 0, 0);

	}



}
