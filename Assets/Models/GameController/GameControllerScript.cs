using UnityEngine;
using System.Collections;

public class GameControllerScript : MonoBehaviour
{
    // main menu GUI
    public GameObject GUImain;
    public GameObject GUIgame;

    // generate boundary
    public float xMin, xMax, yMin, yMax, zMin, zMax;
    float x, y, z;

    // checkpoint generation
    public GameObject anchorpoint;      // for generatation
    private Vector3[] anchorpointList;  // for generatation
    private int anchorPointNum;

    public GameObject checkpoint;
    private int checkpointReachedNum = 0;

    // guide lines
    public GameObject leftGuideLine;
    public GameObject rightGuideLine;
    private GameObject leftGLInstance;
    private GameObject rightGLInstance;
    
    // game status
    public bool isGameStarted = false;

    // Character (Main camera)
    public Camera mainCamera;
    private JumperFirstPersonController jfpc;

    // Boost the Camera
    private float boostDuration = 2.0f;
    private float boostTimer = 0.0f;
    private float finalSpeed = 700; // the speed
    private bool isBoostFinished = false;
    private bool isShakeExecuted;

    // main game object
    private GameObject GameControllerObject;

    // Spaceship (end)
    public GameObject spaceship;
    private Vector3 spaceshipPos;

    // gameover
    private bool isPlayerWin;
    private float distanceTraveled;
    private GameObject gameoverGUI;
    private GameoverGUIScript gameoverScript;
    public GameObject Restarter;


    public void onClick() // Entrance
    {
        // start the game
        Instantiate(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        // game start
        isGameStarted = true;

        // init camera
        mainCamera = Camera.main;
        jfpc = mainCamera.GetComponent<JumperFirstPersonController>();

        // To start the game, controller will automatically generate 4 random checkpoints including a goal in the space.
        // To ensure that all checkpoints are generated for player to move in a general direction rather than move randomly, 
        // hence I divide z direction to 4 parts, so that there will have 4 different sections four generating checkpoint

        // Assume z max is 5000 (it means that one round of SpaceJumper may use less that 60 seconds)
        zMax = 20000 + mainCamera.transform.position.z;
        zMin = 20000 + mainCamera.transform.position.z;

        float zCoor = Random.Range(zMin, zMax);

        // create temp array for storing check point position
        anchorpointList = new Vector3[5];

        // create empty z list
        float[] z = new float[5];

        z[0] = zMax;
        
        for (int i = 1; i < 5; i++)
        {
            z[i] = zCoor / 4.0f * i;
        }

        // create checkpoints
        for (int i = 0; i < 5; i++)
        {   
            if (i != 4) {
                x = Random.Range(xMin, xMax);
                y = Random.Range(yMin, yMax);
            } else
            {
                x = 0;
                y = 0;
                spaceshipPos = new Vector3(x,y,z[i]);
            }
            (Instantiate(anchorpoint, new Vector3(x, y, z[i]), Quaternion.Euler(0, 0, 0)) as GameObject).GetComponent<AnchorPointScript>().setIndex(i);

            // add to position list
            anchorpointList[i] = new Vector3(x, y, z[i]);
        }

        // anchor point
        anchorPointNum = 5;

        // create Spaceship
        Instantiate(spaceship, new Vector3(1000, 0, 21000), Quaternion.Euler(0, 90, 0));

        // init GUI
        // init gameoverGUI
        gameoverGUI = GameObject.FindGameObjectWithTag("gameoverGUI");
        gameoverScript = gameoverGUI.GetComponent<GameoverGUIScript>();

        // LeftDistance
        GameObject leftDistanceToSSGUI = GameObject.FindGameObjectWithTag("uiLeftDisToSS");
        leftDistanceToSSGUI.GetComponent<LeftDistatnceToSSScript>().initGameController(gameObject);

        // create guide lines
        (leftGLInstance = Instantiate(leftGuideLine) as GameObject).GetComponent<LeftGuideLineController>().setPathList(anchorpointList);
        (rightGLInstance = Instantiate(rightGuideLine) as GameObject).GetComponent<RightGuideLineController>().setPathList(anchorpointList);

        // Boost Speed of character
        boostSpeed();
        isShakeExecuted = false;
    }

    void Update()
    {
        // Boosting
        // update timer
        if (!isBoostFinished) {
            if (boostTimer - Time.deltaTime <= 0.0f)
            {
                isBoostFinished = true;
                jfpc.changeInvulnerable(false);
            }
            else
            {
                float tempSpeed = Mathf.Pow(1.0f - boostTimer / boostDuration,2) * finalSpeed;
                mainCamera.GetComponent<JumperFirstPersonController>().setSpeed(tempSpeed);
                boostTimer -= Time.deltaTime;
            }
        }
        if (!isShakeExecuted && boostTimer <= boostDuration/2.0f)
        {
            isShakeExecuted = true;
            jfpc.shakeCamera(5, 10, 1.0f, 2.5f);
        }

        // is player win?
        //Lose
        if (jfpc.getHealth() <= 0)
        {
            // stop movement
            jfpc.setSpeed(2);
            // invulnerable
            jfpc.changeInvulnerable(true);
            // gameover status
            isPlayerWin = false;
            isGameStarted = false;
            // set Player lose
            GameObject.FindGameObjectWithTag("winloseIndicator").GetComponent<WinOrLoseScript>().setPlayerWin(false);
            // show gameover gui
            gameoverScript.gameover();
        }

        if (jfpc.getHealth() > 0 && mainCamera.transform.position.z >= zMax) {
            // stop movement
            jfpc.setSpeed(1);
            // invulnerable
            jfpc.changeInvulnerable(true);
            // gameover status
            isPlayerWin = true;
            isGameStarted = false;
            // set Player lose
            GameObject.FindGameObjectWithTag("winloseIndicator").GetComponent<WinOrLoseScript>().setPlayerWin(true);
            // show gameover gui
            gameoverScript.gameover();
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

    // next anchor point
    public void deleteOneAnchorPoint()
    {
        leftGLInstance.GetComponent<LeftGuideLineController>().deleteFirstPoint();
        rightGLInstance.GetComponent<RightGuideLineController>().deleteFirstPoint();
    }

    public Vector3 getSpaceshipPosition()
    {
        return spaceshipPos;
    }

    public bool getWinOrNot()
    {
        return isPlayerWin;
    }
}
