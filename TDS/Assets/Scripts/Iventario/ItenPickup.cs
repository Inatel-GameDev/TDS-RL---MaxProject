using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItenPickup : Interact
{
    [Header("Objetos Unity")]
    private GameObject protagOBJ;
    private Player protagSRC;
    private GameObject inventarioOBJ;
    private Inventory inventarioSRC;

    public override void Interacting()
    {
        atualizaStatus();
        this.gameObject.SetActive(false);
    }

    private void atualizaStatus()
    {
        protagOBJ = GameObject.Find("Protag");
        inventarioOBJ = GameObject.Find("Inventario");

        protagSRC = protagOBJ.GetComponent<Player>();
        inventarioSRC = inventarioOBJ.GetComponent<Inventory>();

        float pantufas = inventarioSRC.pantufasDeBandidos;
        float tufezin = inventarioSRC.Tufé;

        float playerLife;
        float playerSpeed = (1 + tufezin/2.15f)/10;
        float playerDanoBase;
        float playerIAhelperSize = pantufas;

        protagSRC.iaHelperSize = playerIAhelperSize;
        protagSRC.speed = playerSpeed;

    }

}
