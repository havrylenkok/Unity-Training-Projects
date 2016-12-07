using UnityEngine;
using System.Collections;
using System.Threading;
using System;

public class PauseMenu : MonoBehaviour
{

    public GameObject PauseUI;

    private bool paused = false;

    void Start ()
    {
        PauseUI.SetActive (false);
    }

    void Update ()
    {
        if (Input.GetButtonDown ("Pause")) {
            paused = !paused;
        }
        if (paused) {
            PauseUI.SetActive (true);
            Time.timeScale = 0;
        }
        if (!paused) {
            PauseUI.SetActive (false);
            Time.timeScale = 1; // slow motion = 0.1-0.9
        }
    }

    public void Resume ()
    {
        paused = false;
    }

    public void Restart ()
    {
        Application.LoadLevel (Application.loadedLevel);
    }

    public void MainMenu ()
    {
        // todo: handle main menu
        Application.LoadLevel (0); // file -> build settings - scenes idx
    }

    public void Quit ()
    {
        Application.Quit ();
    }

}
