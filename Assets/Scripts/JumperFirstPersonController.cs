using UnityEngine;
using System.Collections;

public class JumperFirstPersonController : MonoBehaviour {

    /* Variables for jumper*/
    public float movementSpeed = 1.0f;

    public Rigidbody rb;

    void Start () {

        // get rigidbody 
        gameObject.GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        
        rb.MovePosition(transform.position + transform.forward * Time.deltaTime * movementSpeed);

    }

    public void setSpeed(float speed)
    {
        movementSpeed = speed;
    }

}
