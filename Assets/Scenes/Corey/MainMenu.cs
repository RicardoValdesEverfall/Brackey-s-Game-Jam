using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerCam playerCam;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        //TODO: chaneg sence
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
