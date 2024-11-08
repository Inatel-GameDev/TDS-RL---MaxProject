using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SqueletonSwordRework : Enemy
{
    GameObject attackColider;
    private bool changingDirection;
    [SerializeField] private float tryChangeDirectionTime;
    [Header("Stats")]
    int moedasQTD;
    [SerializeField]Rigidbody2D pivo;
    BoxCollider2D colliderATK;

    const string ENEMY_RUN = "Enemy_Run";

    void Start()
    {
        state = 0;
        rb = GetComponent<Rigidbody2D>();
        lookingRight = true;
        moedasQTD = 1;
        sprite = GetComponent<SpriteRenderer>();
        if (sprite == null) Debug.LogError("SpriteRenderer não encontrado!");
        _animator = gameObject.GetComponent<Animator>();
        if (_animator == null) Debug.LogError("Animator não encontrado!");
        playerObj = GameObject.FindWithTag("Player");
        if (playerObj == null) Debug.LogError("Player não encontrado!");
        colliderATK = GetComponent<BoxCollider2D>();
        if (pivo == null) Debug.LogError("Pivo não atribuído no Inspector!");
    }


    void Update()
    {
        //Vendo se o player ta em cena não tenho certeza da necessidade disso
        if (playerObj == null)
        {
            playerObj = GameObject.FindWithTag("Player");
            if (playerObj == null)
            {
                return;
            }
        }

        switch (state)
        {
            case 0://Idle - Procurando player
                if (findPlayer())
                    state = 1;
                if (inRange)
                    state = 2;
                break;
            case 1://Perseguindo
                ChangeAnimationState(ENEMY_RUN);
                if (!findPlayer())
                    state = 0;
                break;
            case 2://Atacando
                findPlayer();
                if (!inRange)
                    state = 1;
                break;

        }

    }
    private void FixedUpdate()
    {

        //Flipando lol
        flipPlayerDirection(lookingRight);
        Vector2 aimDirection = playerObj.transform.position - pivo.transform.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        pivo.MoveRotation(aimAngle);

        switch (state)
        {
            case 0:// IDLE
                Roaming();
                break;
            case 1://perseguindo
                followPlayer();
                break;
            case 2://Batendo no player
                followPlayer();
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
