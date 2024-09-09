using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interact : MonoBehaviour
{
    protected bool inRange = false;
    public bool alreadyInteract = false;


    public interface IInteractable
    {
        void Interact();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            inRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange)
            Interacting();
    }

    public abstract void Interacting();

}
