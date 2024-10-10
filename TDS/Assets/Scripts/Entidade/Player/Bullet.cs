using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [Header("Objetos Unity")]
    public Enemy enemy;
    public PlayerMovement playerScr;
    public GameObject playerObj;

    [Header("Variáveis")]
    float dano;
    bool colidiu = false;
    [SerializeField] float damageBullet;

    public void Awake()
    {
        playerObj = GameObject.Find("Protag");
        playerScr = playerObj.GetComponent<PlayerMovement>();           
        dano = playerScr.danoBase * damageBullet;
                
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null && !colidiu)
        {
            enemy.ReduzirVida(dano);
            // Pra colidir só uma vez
            colidiu = true;
        }
        if (collision.tag == "Wall" || collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }

    }
    
}
