using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : Entity
{
    protected GameObject Player;
    public float detectionRadius; // Raio máximo de detecção
    public int numberOfRays = 36; // Número de raycasts (quanto mais, mais preciso)
    public LayerMask detectionLayer; // Camada que você quer detectar (como o jogador)
    

    protected void followPlayer()
    {

        Vector2 direction;

        direction = (Player.transform.position - transform.position).normalized;
        
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
                if (hit.collider.CompareTag("Player"))
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
        public Vector2 AngleToVector2(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    protected void flipPlayerDirection(bool lokingRight)
    {
        float distX = Player.transform.position.x - rb.position.x;
        if (distX < 0 && !lokingRight)
        {
            //Faz flipar
            lokingRight = true;
        }
        else if (distX > 0 && lokingRight)
        { 
            //Faz Flipar
            lokingRight = false;
        }


    }

}