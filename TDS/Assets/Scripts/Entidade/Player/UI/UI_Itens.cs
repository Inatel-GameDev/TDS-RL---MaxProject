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
    [SerializeField] private GameObject item_storage;
    [SerializeField] private GameObject act;
    [SerializeField] private GameObject[] image_itens;
    public List<GameObject> instantiatedItems = new List<GameObject>();
    private float distH_inicial;//64
    private float distV_inicial;
    private float distH_iten_iten = 180;
    private float distV_iten_iten = -80;
    private float cont_iten = 0;
    [SerializeField]public float delayBetweenDeactivations;
    [SerializeField] private float time_to_intes_desapear;
    [SerializeField] private float time_to_intes_apear;
    public bool is_spawning = false;

    private void Start()
    {
        if (item_interface != null)
        {
            item_interface.SetActive(false); // Certifique-se de que o objeto começa desativado
            initialScale = item_interface.transform.localScale;
        }
        distH_inicial = 33f / 40f;
        distV_inicial = -50f / 45f;
    }

    public void ActivateAndScaleUp()
    {
        StartActivation();
        item_interface_off.SetActive(false);
        if (currentCoroutine != null) StopCoroutine(currentCoroutine); // Interrompe corrotinas anteriores
        item_interface.SetActive(true); // Ativa o objeto
        currentCoroutine = StartCoroutine(ScaleObject(item_interface, initialScale, targetScale, scaleSpeed));
    }

    public void ScaleDownAndDeactivate()
    {
        StartDeactivation();
        if (currentCoroutine != null) StopCoroutine(currentCoroutine); // Interrompe corrotinas anteriores
        currentCoroutine = StartCoroutine(ScaleObject(item_interface, item_interface.transform.localScale, Vector2.zero, scaleSpeed, () =>
        {
            item_interface.SetActive(false); // Desativa o objeto após reduzir a escala
        }));
        item_interface_off.SetActive(true) ;
    }

    private IEnumerator ScaleObject(GameObject obj, Vector2 fromScale, Vector2 toScale,float speed, System.Action onComplete = null)
    {
        float progress = 0f;

        while (progress <= 1f)
        {
            progress += Time.deltaTime * speed;
            Vector2 newScale = Vector2.Lerp(fromScale, toScale, progress);
            obj.transform.localScale = new Vector3(newScale.x, newScale.y, obj.transform.localScale.z); // Preserva o eixo Z
            yield return null; // Aguarda o próximo frame
        }

        obj.transform.localScale = new Vector3(toScale.x, toScale.y, obj.transform.localScale.z); // Garante a escala final
        onComplete?.Invoke(); // Executa o callback, se existir
    }

    public void sapawn_iten_image(string[] itens_name)
    {
        GameObject instanciate_iten;
        int itensPorLinha = 3; // Quantidade de itens por linha

        foreach (string iten in itens_name)
        {
            foreach (GameObject image in image_itens)
            {
                string nome_atual = image.name;
                Transform filho = item_storage.transform.Find(nome_atual+ "(Clone)");
                if (nome_atual == iten && filho == null)
                {
                    // Calcula a posição do item
                    Vector3 posicao = new Vector3(
                        item_storage.transform.position.x + distH_inicial,
                        item_storage.transform.position.y + distV_inicial,
                        item_storage.transform.position.z
                    );

                    // Instancia o item
                    // instanciate_iten = Instantiate(image, posicao, item_storage.transform.rotation);
                    CreateItem(posicao, image);
                    //instanciate_iten.transform.SetParent(item_storage.transform, true);
                    //instanciate_iten.layer = 11;

                    // Atualiza o contador de itens e ajusta as posições
                    cont_iten++;
                    if (cont_iten % (itensPorLinha) == 0)
                    {
                        // Muda para a próxima linha
                        distH_inicial = 33f / 45f; // Reseta a posição horizontal
                        distV_inicial += distV_iten_iten/45f; // Move para a linha de baixo
                    }
                    else
                    {
                        // Incrementa a posição horizontal
                        distH_inicial += distH_iten_iten/45f;
                    }
                }
            }
        }
        if (!act.activeSelf)
            DeactivateAllItems();
    }

    public void CreateItem(Vector3 position, GameObject image)
    {
        // Instanciar o item
        GameObject instanciateItem = Instantiate(image, position, item_storage.transform.rotation);

        // Definir o pai e a camada
        instanciateItem.transform.SetParent(item_storage.transform, true);
        instanciateItem.layer = 11;

        // Adicionar à lista
        instantiatedItems.Add(instanciateItem);
    }

    public void StartDeactivation()
    {
        // Iniciar a desativação dos itens
        StartCoroutine(DeactivateItemsFromEnd());
    }

    private IEnumerator DeactivateItemsFromEnd()
    {
        // Iterar de maior índice para menor
        for (int i = instantiatedItems.Count - 1; i >= 0; i--)
        {
            if (instantiatedItems[i] != null)
            {
                // Desativar o item
                instantiatedItems[i].SetActive(false);
                yield return new WaitForSeconds(time_to_intes_desapear); // Esperar antes de desativar o próximo
            }
        }
        is_spawning = false;
    }

    public void StartActivation()
    {
        if (instantiatedItems.Count > 0)
            is_spawning = true;
        // Iniciar a desativação dos itens
        StartCoroutine(ActivateItemsFromStart());
    }
    private IEnumerator ActivateItemsFromStart()
    {
        // Iterar de maior índice para menor
        for (int i = 0; instantiatedItems.Count > i; i++)
        {
            if (instantiatedItems[i] != null)
            {
                // Desativar o item
                instantiatedItems[i].SetActive(true);
                yield return new WaitForSeconds(time_to_intes_apear); // Esperar antes de desativar o próximo
                
            }
        }
        is_spawning = false;
    }

    public void DeactivateAllItems()
    {
        // Iterar pela lista do maior índice para o menor
        for (int i = instantiatedItems.Count - 1; i >= 0; i--)
        {
            if (instantiatedItems[i] != null)
            {
                instantiatedItems[i].SetActive(false); // Desativar o item
            }
        }
    }

}
