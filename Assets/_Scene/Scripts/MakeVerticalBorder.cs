using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;  //This to be commented out when building
#endif

public class MakeVerticalBorder : MonoBehaviour {

	[SerializeField]
	BorderRenderer br;
	MeshFilter mf;
	[SerializeField]
	Mesh meshFromBorder;
	[SerializeField]
	Color _firstColor;
	[SerializeField]
	Color _secondColor;
	[SerializeField]
	Color _thirdColor;
	MeshRenderer _mRenderer;
	float percent;
	[SerializeField]
	bool _createMesh;
	[SerializeField]
	Mesh[] _meshes;
	public enum MeshSizes{Tiny, Small, Medium, Tall};

	public MeshSizes meshSize;

	// Use this for initialization
	void Start () {
		percent = .25f;
		_mRenderer = GetComponent<MeshRenderer> ();
		mf = GetComponent<MeshFilter> ();

		if(_createMesh)
		{
			br.RegenerateMesh ();
			meshFromBorder = br.CloneBorderMesh;
			mf.mesh = meshFromBorder;
			ObjExporter.MeshToFile (mf, "MyTallerMesh.obj");
			Debug.Log ("Created a mesh Object in root directory");

			var savePath = "Assets/" + "BorderMesh" + ".asset";
			Debug.Log("Saved Mesh to:" + savePath);
#if UNITY_EDITOR
			AssetDatabase.CreateAsset(mf.mesh, savePath);             //This to be commented out when building
			#endif
		}
		else
		{
			mf.mesh = _meshes [(int)meshSize];
		}



		_mRenderer.material.color = _firstColor;
	//	_thirdColor = _firstColor;
	}
	
	// Update is called once per frame
	void Update () {

//		percent += (Time.deltaTime / 2);
//		{
//			if(percent >= 1)
//			{
//				percent = 0;
//				_firstColor = _secondColor;
//				_secondColor = _thirdColor;
//				_thirdColor = _firstColor;
//
//
//			}
//		}
//
//
//		_mRenderer.material.color = Color.Lerp (_firstColor, _secondColor, percent);


	}
}
