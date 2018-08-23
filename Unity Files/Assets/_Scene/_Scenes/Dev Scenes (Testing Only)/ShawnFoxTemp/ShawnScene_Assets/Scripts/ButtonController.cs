using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public bool sceneReloader;
    public GameObject buttonObj;
    public GenericUIWindowScript tutorialWindow;
    bool shownWindow = false;
    public bool buttonPushed;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!buttonPushed)
        {
            if (other.tag == "Table")
            {
                return;
            }
            buttonObj.transform.localPosition = new Vector3(0, 1.35f, 0);
            if (sceneReloader)
            {
                FindObjectOfType<AdditiveSceneMethod.AdditiveSceneController>().CallReload(2);
            }
            else
            {
                if (!shownWindow)
                {
                    tutorialWindow.Show();
                }
                else
                {
                    tutorialWindow.Hide();
                }
                shownWindow = !shownWindow;
            }
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        buttonPushed = false;
        if (other.tag == "Table")
        {
            return;
        }
        buttonObj.transform.localPosition = new Vector3(0, 1.4f, 0);
    }

}