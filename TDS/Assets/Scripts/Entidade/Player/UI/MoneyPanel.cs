using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyPanel : MonoBehaviour
{
    [Header("Objetos Unity")]
    private GameObject inventarioObj;
    private Inventory inventarioScr;
    [SerializeField]private Text moneyQuantity;
    private void Awake()
    {
        inventarioObj = GameObject.Find("Inventario");
        inventarioScr = inventarioObj.GetComponent<Inventory>();
    }
    private void Update()
    {
        PanelUpdate();
    }

    private void PanelUpdate()
    {
        moneyQuantity.text = inventarioScr.moeda.ToString();
    }

}
