
/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
	Author: 		Hayden Reeve
	File:			AdditiveSceneController.cs
	Comments:		THIS SCRIPT IS INCREDIBLY MESSY. You're welcome to use this, but I'll be completely overhauling it in the future to something reusable.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AdditiveSceneMethod {

	/// <summary>
	/// Allows for the manipulation of the Scene Controllers for loading and deloading scenes additively.
	/// </summary>
	class AdditiveSceneController : MonoBehaviour {

		// --- INTERFACE VARIABLES

		[Header("User Configuration")]

		[Tooltip("The names of the available scenes to load. These are not loaded, only selected for possible loading.")]
		[SerializeField] private string[] _astScenesAvailable;

		[Tooltip("The location to spawn the scenes associated with the index number.")]
		[SerializeField] private GameObject[] _agmScenePos;


		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

		// Use this for initialization
		void Start () {

			StartCoroutine(LoadAt (0, _agmScenePos[0].transform.position));
			StartCoroutine(LoadAt (1, _agmScenePos[1].transform.position));
			StartCoroutine(LoadAt (2, _agmScenePos[2].transform.position));

		}


		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

		/// <summary>
		/// Loads itScene at v3Location.
		/// </summary>
		/// <param name="itScene">The Integer of the scene wanted within the Scene Array.</param>
		/// <param name="v3Location">The location of the new Scene</param>
		private IEnumerator LoadAt (int itScene, Vector3 v3Location) {
			
			AsyncOperation aoProcess = SceneManager.LoadSceneAsync (_astScenesAvailable [itScene], LoadSceneMode.Additive);

			while (aoProcess.progress < 0.9) {
				Debug.Log ("Loading Progress: " + aoProcess.progress);
				yield return new WaitForEndOfFrame ();
			}
				
			yield return new WaitForEndOfFrame ();

			GameObject[] agmRoot = SceneManager.GetSceneByName (_astScenesAvailable [itScene]).GetRootGameObjects ();

			if (agmRoot.Length > 1) {
				Debug.LogError ("Cannot manipulate scene. Please parent the entire scene to a single root object.");
			} else {
				GameObject gmRoot = agmRoot [0];
				gmRoot.transform.position = v3Location;
				gmRoot.SetActive (true);
			}

		}

		/// <summary>
		/// Unloads itScene. If the scene is not loaded, will produce an error.
		/// </summary>
		/// <param name="itScene">The integer of the scene wanted within the Total Scene Array.</param>
		private IEnumerator UnloadSceneAt (int itScene) {

			SceneManager.UnloadSceneAsync (_astScenesAvailable [itScene]);
			yield return null;

		}

		/// <summary>
		/// Reloads itScene. If the scene is not loaded, will produce an error and then attempt to load the scene from scratch.
		/// </summary>
		/// <param name="itScene">The Integer of the scene wanted within the Total Scene Array.</param>
		private IEnumerator ReloadSceneAt (int itScene) {

			AsyncOperation aoProcess = SceneManager.UnloadSceneAsync (_astScenesAvailable [itScene]);

			while (!aoProcess.isDone)
				yield return new WaitForEndOfFrame ();

			StartCoroutine(LoadAt(itScene, _agmScenePos[itScene].transform.position));

		}


		/// <summary>
		/// Reloads the SCene
		/// </summary>
		public void CallReload (int it) {

			StartCoroutine(ReloadSceneAt (it));

		}



		/// <summary>
		/// Moves the player to the Arena
		/// </summary>
		public void CallToArena () {

			StartCoroutine(UnloadSceneAt (0));
			StartCoroutine(UnloadSceneAt (1));
			StartCoroutine(UnloadSceneAt (2));

			StartCoroutine(LoadAt (3, _agmScenePos[3].transform.position));
				
		}

		/// <summary>
		/// Moves the player to the Tavern
		/// </summary>
		public void CallToTavern () {

			StartCoroutine(UnloadSceneAt (3));

			StartCoroutine(LoadAt (0, _agmScenePos[0].transform.position));
			StartCoroutine(LoadAt (1, _agmScenePos[1].transform.position));
			StartCoroutine(LoadAt (2, _agmScenePos[2].transform.position));

		}

	}

}