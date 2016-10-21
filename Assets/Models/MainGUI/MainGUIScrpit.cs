using UnityEngine;
using System.Collections;

public class MainGUIScrpit : MonoBehaviour {
    private Canvas CanvasObject; 
                                 
    // Use this for initialization
    void Start () {
        CanvasObject = GetComponent<Canvas>();
    }
	
    public void StartButtonPressed()
    {
        CanvasObject.enabled = false;
    }

    // Update is called once per frame
    void Update () {
    }

    public void setEnable(bool enable)
    {
        CanvasObject.enabled = enable;
    }
}