using UnityEngine;
using System.Collections;

public class QuitBtn : MonoBehaviour
{

    public void onClick()
    {
        // Close game
        Application.Quit();
    }
}