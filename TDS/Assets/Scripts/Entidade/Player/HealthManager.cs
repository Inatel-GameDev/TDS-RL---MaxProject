using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    [SerializeField] private Image healthBar;
    [SerializeField] private PlayerMovement player;

    private void Update()
    {
        HealthBarManager(player.vidaTotal,player.life);
    }

    void HealthBarManager(float maxLife, float life)
    {
        
        float normalizedLife = (life*100f)/(maxLife);
        healthBar.fillAmount = normalizedLife/100f;

    }

}
