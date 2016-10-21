using UnityEngine;
using System.Collections;

public class MeteorController : MonoBehaviour {

    // init metero
    public float xMin, xMax, yMin, yMax, zMin, zMax;
    float x, y, z, rotation1,rotation2,rotation3;
    public GameObject meteor1;
    public GameObject meteor2;
    public Transform meteorSpawn;

    public int numOfMeteor = 5000;

    // Check the position of camera
    public Camera mainCamera;
    private bool shouldDeleteMeteors;
    public int cleanRange;
    private int zCoorFinishedClean;


	// Use this for initialization
	void Start () {

        for(int i = 0;i < numOfMeteor / 2; i++)// num/2 because there are two types of meteor
        {
            // INIT: Const Meteor
            initializeConstantMeteor();
        }

        mainCamera = Camera.main;

        zCoorFinishedClean = 0;
    }

    // Update is called once per frame
    void Update() {

        if (mainCamera.transform.position.z >= (meteorSpawn.position.z + zMax - 1000))
        {
            meteorSpawn.transform.position = new Vector3(0, 0, zMax) + meteorSpawn.transform.position;

            for (int i = 0; i < numOfMeteor / 2; i++)// num/2 because there are two types of meteor
            {
                // INIT: Const Meteor
                initializeConstantMeteor();
            }
        }

        // clean meteors every particular distance
        if (zCoorFinishedClean < (int)(mainCamera.transform.position.z / cleanRange))
        {
            ++zCoorFinishedClean;
            GameObject[] allObjects = (GameObject[])FindObjectsOfType(typeof(GameObject));

            foreach (GameObject objectInstance in allObjects)
            {
                if ((objectInstance.gameObject.name == "Meteor1(Clone)" || objectInstance.gameObject.name == "Meteor2(Clone)") &&
                    objectInstance.gameObject.transform.position.z <= cleanRange * (int)(mainCamera.transform.position.z / cleanRange))
                {
                    Destroy(objectInstance);
                }
            }



        }



    }

    private void initializeConstantMeteor()
    {
        // generate random rotation degree
        rotation1 = Random.Range(0, 360);
        rotation2 = Random.Range(0, 360);
        rotation3 = Random.Range(0, 360);

        // random coordinates
        x = Random.Range(xMin, xMax);
        y = Random.Range(yMin, yMax);
        z = Random.Range(zMin, zMax);

        Instantiate(meteor1, meteorSpawn.position + new Vector3(x, y, z), Quaternion.Euler(rotation1, rotation2, rotation3));

        // generate random rotation degree
        rotation1 = Random.Range(0, 360);
        rotation2 = Random.Range(0, 360);
        rotation3 = Random.Range(0, 360);

        // random coordinates
        x = Random.Range(xMin, xMax);
        y = Random.Range(yMin, yMax);
        z = Random.Range(zMin, zMax);

        Instantiate(meteor2, meteorSpawn.position + new Vector3(x, y, z), Quaternion.Euler(rotation1, rotation2, rotation3));
    }

    public void reset()
    {
        zCoorFinishedClean = 0;
    }
}
