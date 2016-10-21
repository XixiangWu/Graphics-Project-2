using UnityEngine;
using System.Collections;

public class RestartGameControllerScript : MonoBehaviour {


    public GameObject meteorSpawnPosObject;

    public void onClick()
    {
        Instantiate(gameObject);
    }

	// This Controller is used for cleaning everthing from the main scene, reset the transform of the main camera, show main gui;
    // and the distory itself
	void Start () {

        // To restart Everything 
        // 1: hide GameGUI
        GameObject.FindGameObjectWithTag("gameGUI").GetComponent<GameGUIScript>().setEnable(false);


        // 2: clean all the meteros and oen spaceship

        GameObject[] allObjects = (GameObject[])FindObjectsOfType(typeof(GameObject));

        foreach (GameObject objectInstance in allObjects)
        {
            if (objectInstance.gameObject.name == "Meteor1(Clone)" || 
                objectInstance.gameObject.name == "Meteor2(Clone)" ||
                objectInstance.gameObject.name == "MotherShip001(Clone)" ||
                objectInstance.gameObject.name == "GameControllerObject(Clone)" ||
                objectInstance.gameObject.name == "LeftGuideLineDrawer(Clone)" ||
                objectInstance.gameObject.name == "RightGuideLineDrawer(Clone)" ||
                objectInstance.gameObject.name == "AnchorPoint(Clone)" ||
                objectInstance.gameObject.name == "explode(Clone)" ||
                objectInstance.gameObject.name == "explode_2(Clone)"
                )
            {
                Destroy(objectInstance);
            }
        }

        // 3: reset the position and rotation of the player
        GameObject.FindGameObjectWithTag("meteorSpawnPos").transform.position = new Vector3(0, 0, 0);

        Camera.main.GetComponent<JumperFirstPersonController>().reset();
        Camera.main.GetComponent<MeteorController>().reset();

        // 4: hide gameover gui
        GameObject.FindGameObjectWithTag("gameoverGUI").GetComponent<GameoverGUIScript>().setEnable(false);

        // 5: show main gui
        GameObject.FindGameObjectWithTag("mainGUI").GetComponent<MainGUIScrpit>().setEnable(true);

        Destroy(gameObject);
    }
}
