using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FaseTeste : Fase
{
    [SerializeField] private Text bem_vindo_as_cavernas;
    [SerializeField] private GameObject mob_spawn;
    [SerializeField] private MinionsSpawn mob_spawn_src;
    [SerializeField] private ChestSpawner bau_spawn_src;
    void Start()
    {
        max_enemys_easy = 2f;
        max_enemys_medium = 2f;
        max_enemys_hard = 2f;
        numWaves = 999999999999999999f;
        mob_spawn_src = mob_spawn.GetComponent<MinionsSpawn>();
        StartCoroutine(ShowTextOneByOne("Bem vindo ao mundo de teste"));
    }

    private void Update()
    {

        if (bau_spawn_src.getBauCount() == 0)
        {
            foreach (GameObject spawn in bau_spawn_src.bauSpawnpointObjects)
            {
                spawn.SetActive(true);
            }
            bau_spawn_src.spawn();
        }
    }

    IEnumerator ShowTextOneByOne(string texto)
    {
        bem_vindo_as_cavernas.gameObject.SetActive(true);
        Color corInicial = bem_vindo_as_cavernas.color;
        corInicial.a = 255f;
        bem_vindo_as_cavernas.color = corInicial;
        string targetText = texto; // Texto que queremos mostrar
        bem_vindo_as_cavernas.text = ""; // Limpa o texto inicialmente

        for (int i = 0; i < targetText.Length; i++)
        {
            bem_vindo_as_cavernas.text += targetText[i]; // Adiciona uma letra por vez
            yield return new WaitForSeconds(0.03f); // Tempo entre as letras
        }

        yield return new WaitForSeconds(1.5f);
        int passos = 10;
        for (int i = 0; i < passos; i++)
        {
            // Obtém a cor atual do SpriteRenderer
            Color corAtual = bem_vindo_as_cavernas.color;

            // Incrementa a transparência para torná-lo mais "branco"
            corAtual.a = Mathf.Clamp01(corAtual.a - 0.1f);

            // Define a nova cor
            bem_vindo_as_cavernas.color = corAtual;

            // Aguarda um pequeno intervalo antes de continuar o loop
            yield return new WaitForSeconds(0.1f);
        }
        bem_vindo_as_cavernas.gameObject.SetActive(false);
        StartCoroutine(ShowTextOneByOne("Para sair do jogo Morra"));
    }

}
