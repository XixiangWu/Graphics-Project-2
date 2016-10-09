using UnityEngine;
using System.Collections;

public class MeteorController : MonoBehaviour {
    public float xMin, xMax, yMin, yMax, zMin, zMax;
    float x, y, z, rotation;
    public GameObject meteor;
    public Transform meteorSpawn;

	// Use this for initialization
	void Start () {
        for(int i = 0;i < 100; i++)
        {
            rotation = Random.Range(0,360);
            x = Random.Range(xMin, xMax);
            y = Random.Range(yMin, yMax);
            z = Random.Range(zMin, zMax);
            Instantiate(meteor, meteorSpawn.position + new Vector3(x, y, z), Quaternion.Euler(rotation, rotation, rotation));
        } 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
