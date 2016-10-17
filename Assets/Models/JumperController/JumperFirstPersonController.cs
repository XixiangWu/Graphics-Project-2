using UnityEngine;
using System.Collections;

public class JumperFirstPersonController : MonoBehaviour {

    /* Variables for jumper*/
    public float movementSpeed = 1.0f;

    // Collide
    private bool collideAnimation;
    private float collideAnimationTime = 2.5f;
    private float collideAnimationTimer;
    private float collideGoBackwardTime = 2.0f;
    private float collideGoBackwardTimer;
    private float currSpeed;
    private float backwardSpeed;
    public Rigidbody rb;

    // Status
    private int max_health;
    private int health;
    
    void Start () {

        // get rigidbody 
        gameObject.GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        rb.mass = 1;
        rb.useGravity = false;

        // collide animation
        backwardSpeed = -300.0f;
        collideAnimation = false;

        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
        audio.Play(44100);

        // init: status
        max_health = 100;
        health = 100;
    }

    // Update is called once per frame
    void Update () {
        if (collideAnimation)
        {
            float leftTimeAnimation = collideAnimationTime - collideAnimationTimer;

            // 1: Stop and move backward: 1 seconds
            // 2: shake head (from center to left, then left to right, right to left, left to right, right to left, left to center, 6 steps in total)
            // 
            if (leftTimeAnimation <= 1.5f)
            {
                currSpeed = leftTimeAnimation * backwardSpeed;

                // when collide happened, the player should instantly move back a little bit
                rb.MovePosition(transform.position + transform.forward * Time.deltaTime * currSpeed);

                // start a shake head
                if (leftTimeAnimation >= 1.0f)
                {
                    Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, -30, 0) * Time.deltaTime);
                    rb.MoveRotation(deltaRotation * rb.rotation);
                }
            } else if (1.5f < leftTimeAnimation && leftTimeAnimation <= 2.5f)
            {
                if (1.5f < leftTimeAnimation && leftTimeAnimation <= 1.80f)
                {
                    Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 80, 0) * Time.deltaTime);
                    rb.MoveRotation(deltaRotation * rb.rotation);
                } else if (1.80f < leftTimeAnimation && leftTimeAnimation <= 2.00f)
                {
                    Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, -120, 0) * Time.deltaTime);
                    rb.MoveRotation(deltaRotation * rb.rotation);
                } else if (2.0f < leftTimeAnimation && leftTimeAnimation <= 2.20f)
                {
                    Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 120, 0) * Time.deltaTime);
                    rb.MoveRotation(deltaRotation * rb.rotation);
                } else
                {
                    Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, -45, 0) * Time.deltaTime);
                    rb.MoveRotation(deltaRotation * rb.rotation);
                }
            }



        } else
        {
            rb.MovePosition(transform.position + transform.forward * Time.deltaTime * movementSpeed);
        }


        // update all timers
        if (collideAnimation)
        {
            if (collideAnimationTimer - Time.deltaTime <= 0)
            {
                collideAnimationTimer = 0;
                collideAnimation = false;
            } else
            {
                collideAnimationTimer -= Time.deltaTime;
            }
        }
    }

    public void setSpeed(float speed)
    {
        movementSpeed = speed;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        print("ouch! Collider: " + collisionInfo.collider.name);
        Collide(collisionInfo);
    }
    
    // After colliding, a small delay animation comes up;
    void Collide(Collision collisionInfo)
    {
        collideAnimation = true;
        collideAnimationTimer = collideAnimationTime;
        Damage(collisionInfo.collider.name);
    }

    // Damage
    void Damage(string collider)
    {
        if (collider == "Meteor1(Clone)")
        {
            health -= 10;
        } else
        {
            health -= 20;
        }
    }

    public int getHealth()
    {
        return health;
    }

    public int getFullHealth()
    {
        return max_health;
    }
}
