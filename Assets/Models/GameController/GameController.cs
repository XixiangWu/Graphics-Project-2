using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    // main menu GUI
    public GameObject GUImain;

    // generate boundary
    public float xMin, xMax, yMin, yMax, zMin, zMax;
    float x, y, z;

    // checkpoint generation
    public GameObject anchorpoint;      // for generatation
    private Vector3[] anchorpointList;  // for generatation

    public GameObject checkpoint;
    private int checkpointReachedNum = 0;

    // guide lines
    public GameObject leftGuideLine;
    public GameObject rightGuideLine;

    // game status
    public bool isGameStarted = false;

    // Character (Main camera)
    public Camera mainCamera;

    // Boost the Camera
    private float boostDuration = 2.0f;
    private float boostTimer = 0.0f;
    private float finalSpeed = 300;
    private bool isBoostFinished = false;

    // main game object
    public GameObject GameControllerObject;

    public void onClick() // Entrance
    {
        // start the game
        Instantiate(GameControllerObject);
    }

    // Use this for initialization
    void Start()
    {
        // game start
        isGameStarted = true;

        // init camera
        mainCamera = Camera.main;

        // To start the game, controller will automatically generate 4 random checkpoints including a goal in the space.
        // To ensure that all checkpoints are generated for player to move in a general direction rather than move randomly, 
        // hence I divide z direction to 4 parts, so that there will have 4 different sections four generating checkpoint

        // Assume z max is 5000 (it means that one round of SpaceJumper may use less that 60 seconds)
        zMax = 5000;
        zMin = 5000;
        float zCoor = Random.Range(zMin, zMax);

        // create temp array for storing check point position
        anchorpointList = new Vector3[4];

        // create empty z list
        float[] z = new float[4];

        for (int i = 0; i < 4; i++)
        {
            z[i] = zCoor / 4.0f * (i + 1);
        }

        // create checkpoints
        for (int i = 0; i < 4; i++)
        {
            x = Random.Range(xMin, xMax);
            y = Random.Range(yMin, yMax);
            
            Instantiate(anchorpoint, new Vector3(x, y, z[i]), Quaternion.Euler(0, 0, 0));

            // add to position list
            anchorpointList[i] = new Vector3(x, y, z[i]);

            Debug.Log(anchorpointList[i]);
        }

        // create guide lines
        (Instantiate(leftGuideLine) as GameObject).GetComponent<LeftGuideLineController>().setPathList(anchorpointList);
        (Instantiate(rightGuideLine) as GameObject).GetComponent<RightGuideLineController>().setPathList(anchorpointList);

        // Boost Speed of character
        boostSpeed();

    }
        
    void Update()
    {


        // update timer
        if (!isBoostFinished) {
            if (boostTimer - Time.deltaTime <= 0.0f)
            {
                isBoostFinished = true;
            }
            else
            {
                float tempSpeed = (1.0f - boostTimer / boostDuration) * finalSpeed;
                mainCamera.GetComponent<JumperFirstPersonController>().setSpeed(tempSpeed);
                boostTimer -= Time.deltaTime;
            }
        }
    }

    // getter for game status
    public bool getGameStatus()
    {
        return isGameStarted;
    }

    // Boost the speed at the start of the game
    public void boostSpeed()
    {
        boostTimer = boostDuration;
    }
}
