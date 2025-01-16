using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItenInventory : MonoBehaviour
{
    private Inventory inventarioSRC;
    [SerializeField] private Text qtd;
    // Start is called before the first frame update
    void Start()
    {
        inventarioSRC = GameObject.Find("Inventario").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        float pantufas = inventarioSRC.pantufasDeBandidos;
        float tufezin = inventarioSRC.tufé;
        float leite = inventarioSRC.leite;
        float desfribilador = inventarioSRC.desfibrilador;
        float macarroneide = inventarioSRC.macarroneide;
        Debug.Log(gameObject.name);
        if (gameObject.name == "Desfibrilador_iten(Clone)")
        {
            qtd.text = desfribilador.ToString();
        }
        if (gameObject.name == "Leite_iten(Clone)")
        {
            qtd.text = leite.ToString();
        }
        if (gameObject.name == "Macarroneide_iten(Clone)")
        {
            qtd.text = macarroneide.ToString();
        }
        if (gameObject.name == "PantufasDeBandido_iten(Clone)")
        {
            qtd.text = pantufas.ToString();
        }
        if (gameObject.name == "Tufé_iten(Clone)")
        {
            qtd.text = tufezin.ToString();
        }
    }
}
