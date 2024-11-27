using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject pause_menu;
    private GameObject playerObject;
    private Gun gunScript;
    private bool paused;
    public GameDificult dificuldade;
    public string fase;
    [SerializeField] UI_Itens itensUI;
    private bool itens_menu_open;
    public enum GameDificult
    {
        easy = 0,
        medium = 1,
        hard = 2,
    }

    private void Start()
    {
        itens_menu_open = true;
        paused = false;
        // game = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        itensUI = GameObject.Find("UI_ITEN").GetComponent<UI_Itens>();
        playerObject = GameObject.FindWithTag("Player");
        gunScript = playerObject.GetComponent<Gun>();
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            pause(paused);
        }
        if (Input.GetKeyDown(KeyCode.Tab) && !itensUI.is_spawning)
        {
            itens_menu_open = !itens_menu_open;
            if (!itensUI.is_spawning && !itens_menu_open)
            {
                itensUI.ActivateAndScaleUp();
            }
            else if (!itensUI.is_spawning && itens_menu_open)
            {
                itensUI.ScaleDownAndDeactivate();
            }
        }
    }

    private void pause(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0;
            gunScript.canFire = false;
        }
        else
        {
            Time.timeScale = 1;
        }
        pause_menu.SetActive(this.paused);
    }

}
