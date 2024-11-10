using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using static UnityEditor.ShaderData;

// Fazer herança que pegue um script comum fase que tenha o basico de tudo pra poder fazer o minion Spawn pegra a herança


public class Fase0 : Fase
{
    [SerializeField]private Text bem_vindo_as_cavernas; // arraste o componente Text do Canvas aqui no Inspector
    [SerializeField]private GameObject mob_spawn;
    [SerializeField]private MinionsSpawn mob_spawn_src;
    [SerializeField] private GameObject porta;
    private bool siga_em_frente;
    void Start()
    {
        max_enemys_easy = 6f;
        max_enemys_medium = 9f;
        max_enemys_hard = 12f;
        numWaves = 3f;
        mob_spawn_src = mob_spawn.GetComponent<MinionsSpawn>();
        siga_em_frente = false;
        StartCoroutine(ShowTextOneByOne("Bem Vindo as Cavernas"));
        StartCoroutine(Turn_On_Spawns());
    }

    private void Update()
    {
        GameObject[] inimigos_em_cena = GameObject.FindGameObjectsWithTag("Enemy");

        Debug.Log(mob_spawn_src.all_enemys_invoked);
        Debug.Log(inimigos_em_cena.Length);

        if (mob_spawn_src.all_enemys_invoked && inimigos_em_cena.Length == 0)
        {
            // permite sair
            porta.SetActive(true);
            if (siga_em_frente == false)
            {
                StartCoroutine(ShowTextOneByOne("Siga em frente"));
                siga_em_frente=true;
            }
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
    }

    private IEnumerator Turn_On_Spawns()
    {
        yield return new WaitForSeconds(1.6f);
        mob_spawn.SetActive(true);
    }

}
