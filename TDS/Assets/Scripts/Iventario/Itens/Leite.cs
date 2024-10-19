using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leite : ItenPickup
{
    [Header("Objetos Unity")]
    private GameObject inventarioObj;
    private Inventory inventarioScr;
    private GameObject playerOBJ;
    private PlayerMovement playerSRC;
    [Header("Const")]
    private const float shieldLeite = 15f;
    private void Awake()
    {
        inventarioObj = GameObject.Find("Inventario");
        inventarioScr = inventarioObj.GetComponent<Inventory>();
        playerOBJ = GameObject.Find("Protag");
        playerSRC = playerOBJ.GetComponent<PlayerMovement>();
    }


    public override void Interacting()
    {
        inventarioScr.AdicionaLeite();
        playerSRC.aumentaShield(shieldLeite*inventarioScr.leite);
        base.Interacting();
    }
}
