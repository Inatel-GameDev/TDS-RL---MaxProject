using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzdeRetorno : ItenPickup
{
    [Header("Objetos Unity")]
    private GameObject inventarioObj;
    private Inventory inventarioScr;
    private void Awake()
    {
        inventarioObj = GameObject.Find("Inventario");
        inventarioScr = inventarioObj.GetComponent<Inventory>();
    }


    public override void Interacting()
    {
        if (!alreadyInteract)
        {
            alreadyInteract = true;
            inventarioScr.AdicionaDesfibrilador();
            base.Interacting();
        }
    }
}
