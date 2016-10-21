using UnityEngine;
using System.Collections;

public class Meteor1Script : MonoBehaviour
{
    public GameObject explosion;
    public GameObject explosion2;
    public AudioClip audio_exp;
    private AudioSource audioSource;
    public Rigidbody rb;
    private MeshRenderer _renderer;
    private Light _light;
    public Shader shader;
    public Texture texture;
    public Texture normalMap;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<Collider>();

        rb = this.gameObject.GetComponent<Rigidbody>(); // Add the rigidbody.
        rb.mass = 5; // Set the GO's mass to 5 via the Rigidbody.
        rb.useGravity = false;

        // init audio
        audioSource = GetComponent<AudioSource>();

        // give it a random torque;
        Vector3 torque;
        torque.x = Random.Range(-200, 200);
        torque.y = Random.Range(-200, 200);
        torque.z = Random.Range(-200, 200);
        GetComponent<ConstantForce>().torque = torque;

        rb.AddForce(transform.forward * 10.0f);

        _renderer = this.gameObject.GetComponent<MeshRenderer>();
        _light = GameObject.FindWithTag("light").GetComponent<Light>();
        _renderer.material.SetColor("lightColor", _light.color);
        _renderer.material.mainTexture = texture;
        _renderer.material.SetTexture("normalMap", normalMap);
    }

    // Update is called once per frame
    void Update()
    {
        _renderer.material.SetVector("lightPosition", _light.transform.position);
    }

    // collision
    void OnCollisionEnter(Collision collisionInfo)
    {

        if (collisionInfo.collider.name != "Meteor1(Clone)" && collisionInfo.collider.name != "Meteor2(Clone)")
        {
            Instantiate(explosion, rb.transform.position + new Vector3(0,0,50), Quaternion.Euler(0, 0, 0));
            Instantiate(explosion2, rb.transform.position + new Vector3(0, 0, 50), Quaternion.Euler(0, 0, 0));
            audioSource.PlayOneShot(audio_exp, 1f);
            //Destroy(gameObject);
        }
        //print(collisionInfo.collider.name);

        //print("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
        //print("There are " + collisionInfo.contacts.Length + " point(s) of contacts");
        //print("Their relative velocity is " + collisionInfo.relativeVelocity);


    }
}