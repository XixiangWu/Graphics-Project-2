using UnityEngine;
using System.Collections;

public class MeteorController : MonoBehaviour {
    public float xMin, xMax, yMin, yMax, zMin, zMax;
    float x, y, z, rotation1,rotation2,rotation3;
    public GameObject meteor1;
    public GameObject meteor2;
    public Transform meteorSpawn;

    public int numOfMeteor = 100;

	// Use this for initialization
	void Start () {
        for(int i = 0;i < numOfMeteor / 2; i++)// num/2 because there are two types of meteor
        {
            // INIT: Const Meteor
            initializeConstantMeteor();
        } 
	}
	
	// Update is called once per frame
	void Update () {
	    
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
}
