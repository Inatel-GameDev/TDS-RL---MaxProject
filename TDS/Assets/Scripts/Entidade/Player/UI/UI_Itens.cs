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
    [SerializeField] private GameObject act;
    [SerializeField] private GameObject[] image_itens;
    private float distH_inicial;//64
    private float distV_inicial;
    private float distH_iten_iten = 180;
    private float distV_iten_iten = -80;
    private float cont_iten = 0;

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
        GameObject instanciate_iten;

        int itensPorLinha = 3; // Quantidade de itens por linha

        foreach (string iten in itens_name)
        {
            foreach (GameObject image in image_itens)
            {
                string nome_atual = image.name;
                Transform filho = act.transform.Find(nome_atual+ "(Clone)");
                if (nome_atual == iten && filho == null)
                {
                    // Calcula a posição do item
                    Vector3 posicao = new Vector3(
                        act.transform.position.x + distH_inicial,
                        act.transform.position.y + distV_inicial,
                        act.transform.position.z
                    );

                    // Instancia o item
                    instanciate_iten = Instantiate(image, posicao, act.transform.rotation);
                    instanciate_iten.transform.SetParent(act.transform, true);
                    instanciate_iten.layer = 11;

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
    }


}
