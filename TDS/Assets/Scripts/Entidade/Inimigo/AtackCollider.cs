using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackCollider : MonoBehaviour
{
    public PlayerMovement playerScript;
    private GameObject inimigoObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inimigoObject = transform.parent.gameObject;
            inimigoObject.GetComponent<Enemy>().state = 2;
            playerScript.state = 2;
            inimigoObject.GetComponent<Enemy>().inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inimigoObject = transform.parent.gameObject;
        //inimigoObject.GetComponent<Enemy>().state = 0; // Não ta funcionando
        inimigoObject.GetComponent<Enemy>().inRange = false;
    }
    

}
