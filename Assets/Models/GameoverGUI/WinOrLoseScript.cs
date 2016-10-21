using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinOrLoseScript : MonoBehaviour {

    private bool isPlayerWin;
    private Text winOrLoseText;

	// Use this for initialization
	void Start () {
        winOrLoseText = GetComponent<Text>();

        isPlayerWin = true;

        if (isPlayerWin)
        {
            winOrLoseText.text = "You win";
        } else
        {
            winOrLoseText.text = "You died";
        }
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void setPlayerWin(bool isWin)
    {
        isPlayerWin = isWin;
        if (isPlayerWin)
        {
            winOrLoseText.text = "You win";
        }
        else
        {
            winOrLoseText.text = "You died";
        }
    }

}
