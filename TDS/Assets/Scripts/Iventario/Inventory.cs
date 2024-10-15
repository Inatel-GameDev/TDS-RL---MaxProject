using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public int tuf�;
    [SerializeField] public int pantufasDeBandidos;
    [SerializeField] public int moeda;
    [SerializeField] public int leite;
    [SerializeField] public int macarroneide;
    [SerializeField] public int luzDoRetorno;

    public void AdicionaPantufas()
    {
        pantufasDeBandidos++;
    }

    public void AdicionaTuf�()
    {
        tuf�++;
    }

    public void AdicionaMoeda()
    {
        moeda += 1 * pantufasDeBandidos;
    }

    public void AdicionaLeite()
    {
        leite++;
    }
    public void AdicionaMacarroneide()
    {
        macarroneide++;
    }
    public void AdicionaLuzDoRetorno()
    {
        luzDoRetorno++;
    }

}
