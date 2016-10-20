using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;

public class TopTextScript : MonoBehaviour {

    public Text topText;

    private static float updateFrequency = 0.5f;
    private float updateTimer = 0;
    private bool waitToBeChanged = false;

    private string[] pointEndList;
    private int numOfPoints = 0;

	// Use this for initialization
	void Start () {

        // reset the update timer to start
        updateTimer = updateFrequency;

        // point at the end of the sentence
        pointEndList = new string[] { ".","..","...","....","....."};

        topText = GetComponent<Text>();
	}

    // Update is called once per frame
    void Update()
    {
        // update timer
        if (updateTimer - Time.deltaTime < 0.0f)
        {
            if (numOfPoints < 4)
            {
                numOfPoints += 1;
            } else
            {
                numOfPoints = 0;
            }

            waitToBeChanged = true;
            updateTimer = updateFrequency;
        } else
        {
            updateTimer -= Time.deltaTime;
        }


        // change text
        if (waitToBeChanged)
        {
            topText.text = "Updating the shortest path to the next checkpoint" + pointEndList[numOfPoints];
        }

    }

}
