using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SqueletonSword : Enemy
{

    public bool lookingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (life <= 0)
            morrer();
        //Vendo se o player ta em cena
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
            if (Player == null)
            {
                return;
            }
        }

        switch (state)
        {
            case 0://Idle - Procurando player

                if(findPlayer())
                    state = 1;
                break;
            case 1://Perseguindo
                state = 0;
                break;
            case 2://Atacando 
                break;

        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case 0:
                break;
            case 1://perseguindo
                followPlayer();
                break;
            case 2:
                break;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < numberOfRays; i++)
        {
            float angle = i * (360f / numberOfRays);
            Vector2 direction = AngleToVector2(angle);
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + direction * detectionRadius);
        }
    }


}
