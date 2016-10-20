using UnityEngine;
using System.Collections;

public class Meteor2Script : MonoBehaviour
{

    public Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Collider>();
        rb = this.gameObject.GetComponent<Rigidbody>(); // Add the rigidbody.
        rb.mass = 5; // Set the GO's mass to 5 via the Rigidbody.
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // collision
    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.name != "Meteor1(Clone)" && collisionInfo.collider.name != "Meteor2(Clone)")
        {
            print("die");
            //Destroy(gameObject);
        }
        //print("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
       // print("There are " + collisionInfo.contacts.Length + " point(s) of contacts");
        //print("Their relative velocity is " + collisionInfo.relativeVelocity);


    }
}