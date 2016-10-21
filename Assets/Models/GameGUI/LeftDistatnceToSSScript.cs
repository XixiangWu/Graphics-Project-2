using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LeftDistatnceToSSScript : MonoBehaviour {

    private Text leftDistance2SSIndicator;
    private Camera mainCamera;
    private GameObject gameController;
    private Vector3 spaceshipPos;

	// Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        leftDistance2SSIndicator = GetComponent<Text>();
    }

    void Update()
    {
        float xCamera = mainCamera.transform.position.x;
        float yCamera = mainCamera.transform.position.y;
        float zCamera = mainCamera.transform.position.z;
        float xFinal = spaceshipPos.x;
        float yFinal = spaceshipPos.y;
        float zFinal = spaceshipPos.z;

        float leftdis = Mathf.Sqrt(Mathf.Pow(xFinal - xCamera, 2) + Mathf.Pow(yFinal - yCamera, 2) + Mathf.Pow(zFinal - zCamera, 2));

        leftDistance2SSIndicator.text = "Left Distance to Mothership(Enterprise): " + (int)leftdis + " m";
    }

    public void initGameController(GameObject instance)
    {
        spaceshipPos = instance.GetComponent<GameControllerScript>().getSpaceshipPosition();
    }

    public int gethha()
    {
        return 12;
    }
}
