using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour {

    private Text healthText;
    private Camera mainCamera;
    private string player_health;
    private string player_maxHealth;
    private JumperFirstPersonController jfpc;

    // Use this for initialization
    void Start () {
        healthText = GetComponent<Text>();

        // connect with mainCamera
        mainCamera = Camera.main;

        jfpc = mainCamera.GetComponent<JumperFirstPersonController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (jfpc.getFullHealth() != 0)
        {
            healthText.text = "Health: " + jfpc.getHealth() + "/" + jfpc.getFullHealth();
        }

    }
}
