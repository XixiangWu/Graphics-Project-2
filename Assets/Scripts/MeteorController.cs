using UnityEngine;
using System.Collections;

public class MeteorController : MonoBehaviour {
    public float xMin, xMax, yMin, yMax, zMin, zMax;
    float x, y, z, rotation1,rotation2,rotation3;
    public GameObject meteor;
    public Transform meteorSpawn;

    public int numOfMeteor = 100;

	// Use this for initialization
	void Start () {
        for(int i = 0;i < numOfMeteor; i++)
        {
            // generate random rotation degree
            rotation1 = Random.Range(0, 360);
            rotation2 = Random.Range(0, 360);
            rotation3 = Random.Range(0, 360);

            // random coordinates
            x = Random.Range(xMin, xMax);
            y = Random.Range(yMin, yMax);
            z = Random.Range(zMin, zMax);


            // INIT: Const Meteor
            initializeConstantMeteor();


        } 
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    private void initializeConstantMeteor()
    {
        Instantiate(meteor, meteorSpawn.position + new Vector3(x, y, z), Quaternion.Euler(rotation1, rotation2, rotation3));
    }
}
