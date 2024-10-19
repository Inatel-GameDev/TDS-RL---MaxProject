using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeRecharge : MonoBehaviour
{
    [Header("Objetos Unity")]
    private GameObject inventarioObj;
    private Inventory inventarioScr;
    private GameObject playerOBJ;
    private PlayerMovement playerSRC;
    [Header("Const")]
    private const float vidaRegenTime = 3f;
    private float tempoAtual = 0;
    private float tempoAnterior = 0;
    private void Awake()
    {
        inventarioObj = GameObject.Find("Inventario");
        inventarioScr = inventarioObj.GetComponent<Inventory>();
        playerOBJ = GameObject.Find("Protag");
        playerSRC = playerOBJ.GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        float vida = playerSRC.vida;
        float vidaTotal = playerSRC.vidaTotal;
        if (vida < vidaTotal)
        {
            if (tempoAnterior == 0f)
                tempoAnterior = Time.time;
            tempoAtual = Time.time;
            if ((tempoAtual - tempoAnterior) >= vidaRegenTime)
            {
                regenerateLife();
                tempoAnterior = 0f;
            }
        }
    }

    void regenerateLife()
    {
        float vidaRegen = playerSRC.vidaRegen;
        float vidaTotal = playerSRC.vidaTotal;
        float vida = playerSRC.vida;
        if ((vida + vidaRegen) < vidaTotal)
            playerSRC.vida += vidaRegen;
        else
            playerSRC.vida = vidaTotal;
    }

}

