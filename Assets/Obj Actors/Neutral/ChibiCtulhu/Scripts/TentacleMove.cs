using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleMove : MonoBehaviour {

	float _xRot;
	float _yRot;
	float _zRot;
	float xMax;
	float yMax;
	float zMax;
	float xMin;
	float yMin;
	float zMin;
	Vector3 _targetRotation;
	Quaternion _initRotation;
	float _speed;

	float percent;

	Transform tentacleSection;

	// Use this for initialization
	void Start () {
		percent = 0;
		_speed = Random.Range (1, 3);
		tentacleSection = this.transform;
		_initRotation = this.transform.localRotation;
		xMin = tentacleSection.localRotation.eulerAngles.x - 40;
		xMax = tentacleSection.localRotation.eulerAngles.x + 40;
		yMin = tentacleSection.localRotation.eulerAngles.y - 40;
		yMax = tentacleSection.localRotation.eulerAngles.y + 40;
		zMin = tentacleSection.localRotation.eulerAngles.z - 40;
		zMax = tentacleSection.localRotation.eulerAngles.z + 40;

		NewTargetRotation ();


	}
	
	// Update is called once per frame
	void Update () {

		float x, y, z;
		x = transform.localRotation.x;
		y = transform.localRotation.y;
		z = transform.localRotation.z;

		transform.localRotation = Quaternion.Euler (Mathf.Clamp (x, xMin, xMax), Mathf.Clamp (y, yMin, yMax), Mathf.Clamp (z, zMin, zMax));
		//Vector3 transit = Vector3.Lerp (transform.localRotation, _targetRotation, percent);
		transform.localRotation = Quaternion.Lerp (_initRotation, Quaternion.Euler (_targetRotation.x, _targetRotation.y, _targetRotation.z), percent);
		percent += Time.deltaTime/_speed;
		if(percent >= 1)
		{
			NewTargetRotation ();
			percent = 0;
			_speed = Random.Range (1, 3);
		}



	}

	public void NewTargetRotation()
	{
		_targetRotation = new Vector3 (Random.Range (xMin, xMax), Random.Range (yMin, yMax), Random.Range (zMin, zMax));
		_initRotation = transform.localRotation;
		float x, y, z;
		x = _targetRotation.x;
		y = _targetRotation.y;
		z = _targetRotation.z;

		_targetRotation = new Vector3 (Mathf.Clamp (x, xMin, xMax), Mathf.Clamp (y, yMin, yMax), Mathf.Clamp (z, zMin, zMax));
	}

}
