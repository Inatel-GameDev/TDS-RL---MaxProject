using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject pause_menu;
    private bool paused;
    public GameDificult dificuldade;
    public string fase;
    public enum GameDificult
    {
        easy = 0,
        medium = 1,
        hard = 2,
    }

    private void Start()
    {
        paused = false;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            pause(paused);
        }
    }

    private void pause(bool paused)
    {
        if (paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        pause_menu.SetActive(this.paused);
    }

}
