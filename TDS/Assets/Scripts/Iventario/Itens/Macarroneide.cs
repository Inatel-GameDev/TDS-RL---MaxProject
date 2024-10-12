using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Macarroneide : ItenPickup
{
    [Header("Objetos Unity")]
    private GameObject inventarioObj;
    private Inventory inventarioScr;
    private GameObject playerOBJ;
    private PlayerMovement playerSRC;

    [Header("Const")]
    private const float vidaMacarroneide = 15f;

    private void Start()
    {
        inventarioObj = GameObject.Find("Inventario");
        inventarioScr = inventarioObj.GetComponent<Inventory>();
        playerOBJ = GameObject.Find("Protag");
        playerSRC = playerOBJ.GetComponent<PlayerMovement>();
    }


    public override void Interacting()
    {
        playerSRC.aumentaVida(vidaMacarroneide);
        inventarioScr.AdicionaMacarroneide();
        base.Interacting();
    }
}
