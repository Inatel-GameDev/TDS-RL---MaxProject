using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.U2D;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : Entity
{
    [Header("Objetos Unity")]
    protected GameObject playerObj;
    protected Player playerScr;
    public LayerMask detectionLayer; // Camada que você quer detectar (como o jogador)
    [Header("Variáveis")]
    public float detectionRadius; // Raio máximo de detecção
    public float detectionWallRadius;
    public int numberOfRays = 36; // Número de raycasts (quanto mais, mais preciso)
    public bool inRange = false;
    Vector2 randomDirection;
    [SerializeField] float randomMoveDuration;//Duração que ele vai ficar rondando até mudar de rotina
    protected float randomMoveTimer;


    /*
     I wake up to the sounds of the silence that allows
     For my mind to run around with my ear up to the ground
     I'm searching to behold the stories that are told
     When my back is to the world that was smiling when I turned
     */

    protected void followPlayer()
    {

        Vector2 direction;

        direction = (playerObj.transform.position - transform.position).normalized;
        
        position = new Vector2((rb.position.x + (direction.x * speed)), rb.position.y + (direction.y * speed));

        rb.position = position;

    }

    protected bool findPlayer()
    {
        bool playerDetected = false;

        for (int i = 0; i < numberOfRays; i++)
        {
            float angle = i * (360f / numberOfRays);
            Vector2 direction = AngleToVector2(angle);

            // Lança o raycast
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionRadius, detectionLayer);

            // Verifique se o raycast atingiu algo
            if (hit.collider != null)
            {
                // Se o raycast atingir o player e não houver obstáculos no caminho
                if ((hit.collider.CompareTag("Player")) || (hit.collider.CompareTag("PlayerFinder")) )
                {
                    playerDetected = true;
                    
                    break;
                }
                else
                {
                    // Se o raycast atingir um obstáculo, o player não é detectado
                    
                }
            } 
        }
        return playerDetected;
    }
    protected bool hitingWall()
    {
        bool wallDetected = false;
        for (int i = 0; i < numberOfRays; i++)
        {
            float angle = i * (360f / numberOfRays);
            Vector2 direction = AngleToVector2(angle);

            // Lança o raycast
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectionWallRadius, detectionLayer);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    wallDetected = true;
                }
            }
        }
        return wallDetected;
    }
    public Vector2 AngleToVector2(float angle)
    {   //Essa função serve pra calucular a distancia dos raycasts
        float radian = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    protected void flipPlayerDirection(bool lokingRight)
    {
       
        float distX = playerObj.transform.position.x - rb.position.x;
        if (transform != null)
        {
            Vector3 enemyScale = transform.localScale;
            if (distX < 0)
            {
                //Faz flipar
                transform.eulerAngles = new Vector3(enemyScale.x, 180, enemyScale.z);
                lokingRight = false;
            }
            else if (distX > 0)
            {
                //Faz Flipar
                transform.eulerAngles = new Vector3(enemyScale.x, 0, enemyScale.z);
                lokingRight = true;
            }

        }
    }

    protected void Roaming() 
    {
        // Se o tempo de movimento aleatório acabou, escolha uma nova direção aleatória
        if (randomMoveTimer <= 0)
        {
            ChooseNewRandomDirection();
        }

        // Mova o inimigo na direção aleatória escolhida
        transform.Translate(randomDirection * speed);

        // Reduza o temporizador
        randomMoveTimer -= Time.deltaTime;
    }
    
    void ChooseNewRandomDirection()
    {
        // Escolha uma direção aleatória
        randomDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;

        // Resete o temporizador para a duração do movimento aleatório
        randomMoveTimer = randomMoveDuration;
    }

    protected void damageToPlayer(float dano)
    {
        playerScr = playerObj.GetComponent<Player>();
        playerScr.ReduzirVida(dano);
    }

    public IEnumerator RepeatAtack(float initialDelay, float repeatRate,float dano)
    {
        yield return new WaitForSeconds(initialDelay);
        while (inRange == true)
        {
            damageToPlayer(dano);
            yield return new WaitForSeconds(repeatRate);
            //Debug.Log("To colidindo");
        }
    }

}