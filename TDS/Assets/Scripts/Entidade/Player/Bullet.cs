using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damageBullet;
    public Enemy enemy;
    public PlayerMovement playerScr;
    public GameObject playerObj;
    float dano;
    bool colidiu = false;

    public void Awake()
    {
        playerObj = GameObject.Find("Protag");
        playerScr = playerObj.GetComponent<PlayerMovement>();           
        dano = playerScr.danoBase * damageBullet;
                
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "PlayerFinder" && collision.tag != "AttackColider") 
            Destroy(gameObject);
        enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null && !colidiu)
        {
            enemy.ReduzirVida(dano);
            colidiu = true;
        }

    }
    
}
