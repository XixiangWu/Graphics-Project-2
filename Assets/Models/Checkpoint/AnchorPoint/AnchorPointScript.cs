using UnityEngine;
using System.Collections;

public class AnchorPointScript : MonoBehaviour {

    private int indexAnchorPoint;
    public Camera mainCamera;
    private GameObject gameController;

	// Use this for initialization
	void Start () {
        mainCamera = Camera.main;

        gameController = GameObject.FindGameObjectWithTag("MainGameController");
	}
	
	// Update is called once per frame
	void Update () {
	    if (mainCamera.transform.position.z >= transform.position.z - 500)
        {
            gameController.GetComponent<GameControllerScript>().deleteOneAnchorPoint();
            Destroy(this);
        }
	}

    public void setIndex(int index)
    {
        indexAnchorPoint = index;
    } 

    public int getIndex(int index)
    {
        return indexAnchorPoint;
    }
}
