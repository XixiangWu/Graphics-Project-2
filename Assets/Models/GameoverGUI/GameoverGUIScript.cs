using UnityEngine;
using System.Collections;

public class GameoverGUIScript : MonoBehaviour {

    private Canvas canvasObject;

	// Use this for initialization
	void Start () {
        canvasObject = GetComponent<Canvas>();

        canvasObject.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void gameover()
    {
        // show this gui;
        canvasObject.enabled = true;
    }

    public void setEnable(bool enable)
    {
        canvasObject.enabled = enable;
    }

}
