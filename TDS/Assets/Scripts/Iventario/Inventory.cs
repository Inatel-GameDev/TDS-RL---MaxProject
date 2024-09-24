using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int Tufé;
    public int pantufasDeBandidos;

    public void AdicionaPantufas()
    {
        pantufasDeBandidos++;
    }

    public void AdicionaTufé()
    {
        Tufé++;
    }

}
