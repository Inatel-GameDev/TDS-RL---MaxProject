using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] public int tufé;
    [SerializeField] public int pantufasDeBandidos;
    [SerializeField] public int moeda;
    [SerializeField] public int leite;
    [SerializeField] public int macarroneide;
    [SerializeField] public int desfibrilador;
    [SerializeField] public int fibrilador;
    public string[] itens_name;

    /*
    Nomes dos itens
    Desfibrilador
    Leite
    Macarroneide
    PantufasDeBandido
    Tufé
    "Fibrilador"
    */

    private void Start()
    {
        adiciona_intem_name();
    }

    public void adiciona_intem_name()
    {
        Array.Clear(itens_name, 0, itens_name.Length);
        if (tufé > 0)
        {
            Array.Resize(ref itens_name, itens_name.Length + 1);
            itens_name[itens_name.Length - 1] = "Tufé_iten";
        }
        if (pantufasDeBandidos > 0)
        {
            Array.Resize(ref itens_name, itens_name.Length + 1);
            itens_name[itens_name.Length - 1] = "PantufasDeBandido_iten";
        }
        if (leite > 0)
        {
            Array.Resize(ref itens_name, itens_name.Length + 1);
            itens_name[itens_name.Length - 1] = "Leite_iten";
        }
        if (macarroneide > 0)
        {
            Array.Resize(ref itens_name, itens_name.Length + 1);
            itens_name[itens_name.Length - 1] = "Macarroneide_iten";
        }
        if (desfibrilador > 0)
        {
            Array.Resize(ref itens_name, itens_name.Length + 1);
            itens_name[itens_name.Length - 1] = "Desfibrilador_iten";
        }
        if (fibrilador > 0)
        {
            Array.Resize(ref itens_name, itens_name.Length + 1);
            itens_name[itens_name.Length - 1] = "Fibrilador_iten";
        }
    }

    public void AdicionaPantufas()
    {
        pantufasDeBandidos++;
        adiciona_intem_name();
    }

    public void AdicionaTufé()
    {
        tufé++;
        adiciona_intem_name();
    }

    public void AdicionaMoeda(int qtd)
    {
        moeda += qtd * (pantufasDeBandidos + 1);
    }

    public void AdicionaLeite()
    {
        leite++;
        adiciona_intem_name();
    }
    public void AdicionaMacarroneide()
    {
        macarroneide++;
        adiciona_intem_name();
    }
    public void AdicionaDesfibrilador()
    {
        desfibrilador++;
        adiciona_intem_name();
    }
    public void AdicionaFibrilador()
    {
        fibrilador++;
        adiciona_intem_name();
    }

}
