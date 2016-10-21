using UnityEngine;
using System.Collections;
using EZCameraShake;

public class JumperFirstPersonController : MonoBehaviour {

    /* Variables for jumper*/
    public float movementSpeed = 1.0f;

    // Collide
    private bool collideAnimation;
    private float collideAnimationTime = 2.5f;
    private float collideAnimationTimer;
    private float collideGoBackwardTimer;
    private float reboostDuration = 0.7f;
    private float reboostTimer;
    private bool isReboosting;
    private float currSpeed;
    private float backwardSpeed;
    public Rigidbody rb;
    private bool shakeBool;
    private float generealSpeed;

    // Status
    private int max_health;
    private int health;
    private string status;
    private bool isInVulnerable;

    void Start () {

        // get rigidbody 
        gameObject.GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        rb.mass = 1;
        rb.useGravity = false;

        // collide animation
        backwardSpeed = -300.0f;
        collideAnimation = false;
        shakeBool = false;

        // init: status
        max_health = 100;
        health = 100;

        // status: Normal
        status = "normal";
        isInVulnerable = true;
    }

    // Update is called once per frame
    void Update () {

        if (collideAnimation && health > 0)
        {
            float leftTimeAnimation = collideAnimationTime - collideAnimationTimer;
            if (shakeBool) {
                CameraShaker.Instance.ShakeOnce(5, 10, 0, 1.5f);
                shakeBool = false;
            }
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
                    Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, -40, 0) * Time.deltaTime);
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
                    Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, -25, 0) * Time.deltaTime);
                    rb.MoveRotation(deltaRotation * rb.rotation);

                    // start reboosting
                    reboostTimer = reboostDuration;
                    isReboosting = true;

                }
            }

            generealSpeed = currSpeed;

        } else
        {
            if (isReboosting)
            {
                currSpeed = reboostSpeed();
                rb.MovePosition(transform.position + transform.forward * Time.deltaTime * currSpeed);
                generealSpeed = currSpeed;
            } else {
                rb.MovePosition(transform.position + transform.forward * Time.deltaTime * movementSpeed);
                generealSpeed = movementSpeed;
            }
        }

        // update all timers
        if (collideAnimation)
        {
            if (collideAnimationTimer - Time.deltaTime <= 0)
            {
                collideAnimationTimer = 0;
                collideAnimation = false;
                status = "normal";
            } else
            {
                collideAnimationTimer -= Time.deltaTime;
            }
        }
        if (isReboosting)
        {
            if (reboostTimer - Time.deltaTime <= 0)
            {
                reboostTimer = 0;
                isReboosting = false;
            } else
            {
                reboostTimer -= Time.deltaTime;
            }
        }

    }

    public void setSpeed(float speed)
    {
        movementSpeed = speed;
    }

    public int getSpeedInInteger()
    {
        return (int)generealSpeed;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (!isInVulnerable) {
            print("ouch! Collider: " + collisionInfo.collider.name);
            status = "damaged";
            shakeBool = true;
            Collide(collisionInfo);
        }
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
            if (health - 10 <= 0) { health = 0; } else { health -= 10; }
        } else
        {
            if (health - 20 <= 0) { health = 0; } else { health -= 20; }
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

    public string getCurrentStatus()
    {
        return status;
    }
    public void shakeCamera(float magnitude, float roughness, float fadeInTime, float fadeOutTime)
    {
        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
    }

    public void changeInvulnerable(bool isInvulnerableBool)
    {
       isInVulnerable = isInvulnerableBool;
    }

    private float reboostSpeed()
    {
        return Mathf.Pow(1.0f - reboostTimer, 2) * movementSpeed;
    }

    public void reset()
    {
        rb.MovePosition(new Vector3(0,0,0));
        rb.MoveRotation(Quaternion.Euler(new Vector3(0f, 0f, 0f)));

        health = 100;

        status = "normal";
        isInVulnerable = true;
    }
}
