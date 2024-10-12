using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leite : ItenPickup
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
        inventarioScr.AdicionaLeite();
        base.Interacting();
    }
}
