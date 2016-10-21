using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameGUIScript : MonoBehaviour {
    
    private Canvas CanvasObject;
    private Camera mainCamera;
    private JumperFirstPersonController jfpc;

    // element in GameGUI
    public Text flyingIndicator;
    public Text healthIndicator;
    public Text speedIndicator;
    public Text leftdisIndicator;


    // This is the game GUI script, it is used for indicating the Cooldown, Health, and other fantastic 
    // effects

    void Start () {

        // init canvas
        CanvasObject = GetComponent<Canvas>();
        CanvasObject.enabled = false;

        // init camera
        mainCamera = Camera.main;


        // init camara script
        jfpc = Camera.main.GetComponent<JumperFirstPersonController>();
    }
	
    // The game officailly starts, show this canvas
    public void StartButtonPressed()
    {
        // show GUI
        CanvasObject.enabled = true;
    }

    // Update is called once per frame
    void Update () {
        // shake the screen
        if (jfpc.getCurrentStatus() == "damaged")
        {
            
        }



    }

    public void setEnable(bool enable)
    {
        CanvasObject.enabled = enable;
    }

}