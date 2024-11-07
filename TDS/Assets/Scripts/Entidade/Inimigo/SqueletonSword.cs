using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Android;

public class SqueletonSword : Enemy
{
    [SerializeField] float inicialTimeAtk;
    [SerializeField] float repitTimeAtk;
    GameObject attackColider;
    private bool changingDirection;
    [SerializeField] private float tryChangeDirectionTime;
    [Header("Stats")]
    int moedasQTD;

    const string ENEMY_RUN = "Enemy_Run";

    // Start is called before the first frame update
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

        if(inRange == false)
        {
            StopCoroutine(RepeatAtack(inicialTimeAtk, repitTimeAtk, danoBase));
        }

        switch (state)
        {
            case 0://Idle - Procurando player

                if (findPlayer())
                    state = 1;
                break;
            case 1://Perseguindo
                ChangeAnimationState(ENEMY_RUN);
                state = 0;
                break;
            case 2://Atacando 
                StartCoroutine(RepeatAtack(inicialTimeAtk, repitTimeAtk, danoBase));
                state = 0;
                break;

        }
    }

    private void FixedUpdate()
    {

        //Flipando lol
        flipPlayerDirection(lookingRight);

        switch (state)
        {
            case 0:
                if (!findPlayer())
                    Roaming();
                if (hitingWall() && changingDirection == false)
                    StopCoroutine(tryOderDirection());
                break;
            case 1://perseguindo
                followPlayer();
                break;
            case 2://Batendo no player
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

    private IEnumerator tryOderDirection() 
    {
        changingDirection = true;
        randomMoveTimer = 0f;
        yield return new WaitForSeconds(tryChangeDirectionTime);
        changingDirection = false;
    }

}
