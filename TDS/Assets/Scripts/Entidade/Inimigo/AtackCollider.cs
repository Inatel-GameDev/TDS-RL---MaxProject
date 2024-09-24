using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackCollider : MonoBehaviour
{
    public GameObject playerObject;
    private GameObject inimigoObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inimigoObject = transform.parent.gameObject;
            inimigoObject.GetComponent<Enemy>().state = 2;
            Debug.Log("Entrou");
            inimigoObject.GetComponent<Enemy>().inRange = true;
            Debug.Log("Passou");
            playerObject.GetComponent<PlayerMovement>().state = 2;
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inimigoObject = transform.parent.gameObject;
            //inimigoObject.GetComponent<Enemy>().state = 0; // Não ta funcionando
            inimigoObject.GetComponent<Enemy>().inRange = false;
            Debug.Log("saiu");
        }
    }
    

}
