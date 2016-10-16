using UnityEngine;
using System.Collections;

public class GameGUIScript : MonoBehaviour {
    
    private Canvas CanvasObject; 
                                 
    // This is the game GUI script, it is used for indicating the Cooldown, Health, and other fantastic 
    // effects

    void Start () {
        CanvasObject = GetComponent<Canvas>();
        CanvasObject.enabled = false;
    }
	
    // The game officailly starts, show this canvas
    public void StartButtonPressed()
    {
        // show GUI
        CanvasObject.enabled = true;
    }

    // Update is called once per frame
    void Update () {
    }
}