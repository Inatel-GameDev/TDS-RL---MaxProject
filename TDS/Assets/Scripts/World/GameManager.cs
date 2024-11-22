using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject pause_menu;
    private bool paused;
    public GameDificult dificuldade;
    public string fase;
    [SerializeField] UI_Itens itensUI;
    private bool itens_menu_open = false;
    public enum GameDificult
    {
        easy = 0,
        medium = 1,
        hard = 2,
    }

    private void Start()
    {
        paused = false;
        // game = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        itensUI = GameObject.Find("UI_ITEN").GetComponent<UI_Itens>();
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            pause(paused);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            itens_menu_open = !itens_menu_open;
            if (itens_menu_open)
                itensUI.ScaleDownAndDeactivate();
            else
                itensUI.ActivateAndScaleUp();
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
