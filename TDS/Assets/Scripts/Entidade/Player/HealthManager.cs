using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    [SerializeField] private Image healthBar;
    [SerializeField] private Player player;

    private void Update()
    {
        HealthBarManager(100,player.life);
    }

    void HealthBarManager(float maxLife, float life)
    {
        
        float normalizedLife = (life*100f)/maxLife;

        healthBar.fillAmount = normalizedLife/100f;

    }

}
