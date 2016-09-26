using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/MobileTouchController")]
public class  MobileTouchController : MonoBehaviour
{
	public Vector2 startPos;
	public Vector2 direction;
	public bool directionChosen;

	private Quaternion rotationX;
	private Quaternion rotationY;

	public float rotateSpeed;

	void Update()
	{
		if (Input.touchCount > 0) {

			// get finger moving direction
			Touch touch = Input.GetTouch (0);
			switch (touch.phase) {
			case TouchPhase.Began:
				startPos = touch.position;
				directionChosen = false;
				break;

			case TouchPhase.Moved:
				startPos = touch.position;
				direction = touch.position - startPos;
				break;
			}
				
			// Move 
			if (direction.x > 0.0f) {
				transform.Rotate (rotationX.eulerAngles, rotateSpeed * Time.deltaTime);
			} 

			if (direction.x < 0.0f) {
				transform.Rotate (-rotationX.eulerAngles, rotateSpeed * Time.deltaTime);
			}
				
			if (direction.y > 0.0f) {
				transform.Rotate (rotationY.eulerAngles, rotateSpeed * Time.deltaTime);
			}

			if (direction.y < 0.0f) {
				transform.Rotate (-rotationY.eulerAngles, rotateSpeed * Time.deltaTime);
			}

		}

	}

	void Start()
	{
		if (GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().freezeRotation = true;
		}

		rotationX = Quaternion.Euler(new Vector3(0, 30, 0));
		rotationY = Quaternion.Euler(new Vector3(0, 0, 30));
		rotateSpeed = 50.0f;
	}

	//******************************************************************

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 5f;
	public float sensitivityY = 5f;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public float minimumY = -60F;
	public float maximumY = 60F;
}