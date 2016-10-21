using UnityEngine;
using System.Collections;

// Activate head tracking using the gyroscope
public class GyroscopeController : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody rb;
    Gyroscope gyro;

    // Use this for initialization
    void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();

        Input.gyro.enabled = true;
        gyro = Input.gyro;


    }

    // Update is called once per frame
    void Update()
	{
		rb.MoveRotation(gyro.attitude);
    }

	public void switchStatus() {
		GetComponent<GyroscopeController> ().enabled = !GetComponent<GyroscopeController> ().enabled;
	}
}
