using UnityEngine;
using System.Collections;

public class QuitBtn : MonoBehaviour
{

    public void onClick()
    {
        // Save game data

        // Close game
        Debug.Log(123);
        Application.Quit();
    }
}