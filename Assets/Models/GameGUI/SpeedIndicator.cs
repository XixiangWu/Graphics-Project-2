using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeedIndicator : MonoBehaviour {

    private Text speedIndicator;
    private JumperFirstPersonController jfpc;

	// Use this for initialization
	void Start () {

        jfpc = Camera.main.GetComponent<JumperFirstPersonController>();

        speedIndicator = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        speedIndicator.text = jfpc.getSpeedInInteger() + "m/s";

	}
}
