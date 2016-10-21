using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        // mouse movement detection
        float h = 1.25f * Input.GetAxis("Mouse X");
        float v = -1.25f * Input.GetAxis("Mouse Y");
        rb.transform.Rotate(v, h, 0);
    }
}
