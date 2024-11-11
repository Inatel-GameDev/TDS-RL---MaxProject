using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.ShaderData;

public abstract class Interact : MonoBehaviour
{
    [SerializeField]protected bool inRange = false;
    public bool alreadyInteract = false;
    [SerializeField] private SpriteRenderer spriteRenderer;
    protected bool its_destroying = false;

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
        if (Input.GetKeyDown(KeyCode.E) && inRange && !its_destroying)
            Interacting();
    }

    public abstract void Interacting();

    protected IEnumerator Destroy_it()
    {
        int passos = 15;
        for (int i = 0; i < passos; i++)
        {
            // Obtém a cor atual do SpriteRenderer
            Color corAtual = spriteRenderer.color;

            // Incrementa a transparência para torná-lo mais "branco"
            corAtual.a = Mathf.Clamp01(corAtual.a - 0.1f);

            // Define a nova cor
            spriteRenderer.color = corAtual;

            // Aguarda um pequeno intervalo antes de continuar o loop
            yield return new WaitForSeconds(0.1f);
        }
        this.gameObject.SetActive(false);
    }
}
