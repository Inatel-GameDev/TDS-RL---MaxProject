using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Itens : MonoBehaviour
{
    [Header("Interface dos itens")]
    public GameObject item_interface; // O objeto que será escalado
    public GameObject item_interface_off;
    public Vector2 targetScale; // Escala final desejada
    private Vector2 initialScale; // Escala inicial para referência
    public float scaleSpeed = 2f; // Velocidade da transição de escala
    private Coroutine currentCoroutine; // Para interromper corrotinas em execução
    [Header("Itens")]
    [SerializeField] private GameObject[] image_itens;
    private float distH_inicial = 64f;
    private float distV_inicial = -55;
    private float distH_iten_iten = 130f;
    private float distV_iten_iten = -55;

    private void Start()
    {
        if (item_interface != null)
        {
            item_interface.SetActive(false); // Certifique-se de que o objeto começa desativado
            initialScale = item_interface.transform.localScale;
        }
    }

    public void ActivateAndScaleUp()
    {
        item_interface_off.SetActive(false);
        if (currentCoroutine != null) StopCoroutine(currentCoroutine); // Interrompe corrotinas anteriores
        item_interface.SetActive(true); // Ativa o objeto
        currentCoroutine = StartCoroutine(ScaleObject(item_interface, initialScale, targetScale));
    }

    public void ScaleDownAndDeactivate()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine); // Interrompe corrotinas anteriores
        currentCoroutine = StartCoroutine(ScaleObject(item_interface, item_interface.transform.localScale, Vector2.zero, () =>
        {
            item_interface.SetActive(false); // Desativa o objeto após reduzir a escala
        }));
        item_interface_off.SetActive(true) ;
    }

    private IEnumerator ScaleObject(GameObject obj, Vector2 fromScale, Vector2 toScale, System.Action onComplete = null)
    {
        float progress = 0f;

        while (progress <= 1f)
        {
            progress += Time.deltaTime * scaleSpeed;
            Vector2 newScale = Vector2.Lerp(fromScale, toScale, progress);
            obj.transform.localScale = new Vector3(newScale.x, newScale.y, obj.transform.localScale.z); // Preserva o eixo Z
            yield return null; // Aguarda o próximo frame
        }

        obj.transform.localScale = new Vector3(toScale.x, toScale.y, obj.transform.localScale.z); // Garante a escala final
        onComplete?.Invoke(); // Executa o callback, se existir
    }

    public void sapawn_iten_image(string[] itens_name) 
    {
        foreach(string iten in itens_name)
        {
            foreach (GameObject image in image_itens)
            {
                string nome_atual = image.name;
                if (nome_atual == iten)
                {
                    GameObject instanciate_iten = Instantiate(image.gameObject, 
                       new Vector3( (item_interface.transform.position.x+distH_inicial),
                       item_interface.transform.position.x + distV_inicial, 
                       item_interface.transform.position.z),
                       item_interface.transform.rotation);
                    instanciate_iten.layer = 11;
                }
            }
        }
    }

}
