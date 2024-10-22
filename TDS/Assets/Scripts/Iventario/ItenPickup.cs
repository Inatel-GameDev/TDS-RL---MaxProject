using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
        float tufezin = inventarioSRC.tufé;
        float leite = inventarioSRC.leite;

        float playerLife;
        float playerSpeed = 0.1f + math.log2(tufezin+1)/36;
        float playerDanoBase;
        float playerIAhelperSize = pantufas;
        float shield = leite * 15;

        protagSRC.iaHelperSize = playerIAhelperSize;
        protagSRC.speed = playerSpeed;
        protagSRC.shieldTotal = shield;
    }

}
