using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image shieldBar;
    [SerializeField] private Text lifeTxt;
    private PlayerMovement playerSRC;

    private void Start()
    {
        // Encontra o objeto Player uma vez no Start
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerSRC = playerObj.GetComponent<PlayerMovement>();
        }
        else
        {
            Debug.LogError("Objeto com a tag 'Player' não encontrado!");
        }
    }

    private void Update()
    {
        // Verifica se playerSRC foi encontrado antes de chamar os métodos
        if (playerSRC != null)
        {
            HealthBarManager(playerSRC.vidaTotal, playerSRC.vida, playerSRC.shield);
            ShieldBarManager(playerSRC.vidaTotal, playerSRC.shield);
        }
    }

    void HealthBarManager(float maxLife, float life, float shildTotal)
    {
        float normalizedLife = (life * 100f) / maxLife;
        if (healthBar != null)
        {
            healthBar.fillAmount = normalizedLife / 100f;
        }
        if((lifeTxt != null) && shildTotal > 0)
        {
            lifeTxt.text = life + "+<color=#66CCFF>" + shildTotal + "</color>/" + maxLife;
        }
        else  if (lifeTxt != null)
        {
            lifeTxt.text = life + "/" + maxLife;
        }
    }

    void ShieldBarManager(float maxLife, float shield)
    {
        float normalizedShield = (shield * 100f) / maxLife;
        if (shieldBar != null)
        {
            shieldBar.fillAmount = normalizedShield/100f;
        }
        else
        {
            Debug.Log("Po nn ta achando a barra");
        }
    }
}
