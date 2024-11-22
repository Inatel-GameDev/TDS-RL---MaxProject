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
    private UI_Itens itensUI;
    public override void Interacting()
    {
            atualizaStatus();
            StartCoroutine(Destroy_it());
    }

    private void atualizaStatus()
    {
        protagOBJ = GameObject.Find("Protag");
        inventarioOBJ = GameObject.Find("Inventario");
        itensUI = GameObject.Find("UI_ITEN").GetComponent<UI_Itens>();

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

        itensUI.sapawn_iten_image(inventarioSRC.itens_name);

    }

}
