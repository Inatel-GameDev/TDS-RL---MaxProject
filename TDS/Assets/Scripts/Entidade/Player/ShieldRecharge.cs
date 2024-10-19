using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldRecharge : MonoBehaviour
{
    [Header("Objetos Unity")]
    private GameObject inventarioObj;
    private Inventory inventarioScr;
    private GameObject playerOBJ;
    private PlayerMovement playerSRC;
    [Header("Const")]
    private const float shieldRegenTime = 5f;
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
        float shield = playerSRC.shield;
        float shieldTotal = playerSRC.shieldTotal;
        if (shield < shieldTotal)
        {
            if (tempoAnterior == 0f)
                tempoAnterior = Time.time;
            tempoAtual = Time.time;
            if ((tempoAtual - tempoAnterior) >= shieldRegenTime)
            {
                regenerateShield();
                tempoAnterior = 0f;
            }
        }
    }

    void regenerateShield()
    {
        float shieldRegen = playerSRC.shieldRegen;
        float shieldTotal = playerSRC.shieldTotal;
        float shield = playerSRC.shield;
        float leite = inventarioScr.leite;
        Debug.Log(shieldTotal);
        if ((shield + (shieldRegen * leite)) < shieldTotal)
            playerSRC.shield += shieldRegen * leite;
        else
            playerSRC.shield = shieldTotal;
    }

}
