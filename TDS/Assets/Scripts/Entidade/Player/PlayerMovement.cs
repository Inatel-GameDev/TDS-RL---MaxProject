//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovement : Player
{
    [Header("Objetos Unity")]
    public Gun gun;
    public Rigidbody2D centerRb;

    [Header("Variáveis")]
    Vector2 m_Position;
    private bool canDash = true;
    private bool isDashing = false;

    [Header("Dash")]
    public float tempoDash = 1f;
    public float poderDash = 3f;
    public float cooldownDash = 5f;

    //Constantes animação
    const string PLAYER_IDLE = "PlayerIdle";
    const string PLAYER_RUN = "Player_Run";
    const string PLAYER_JUMP = "Player_Jump";
    const string PLAYER_FALL = "Player_Fall";
    const string PLAYER_DAMAGE = "Player_damage";
    const string PLAYER_ATTACK = "Player_Attack";


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("Rigidbody2D não encontrado!");

        sprite = GetComponent<SpriteRenderer>();
        if (sprite == null) Debug.LogError("SpriteRenderer não encontrado!");

        _animator = gameObject.GetComponent<Animator>();
        if (_animator == null) Debug.LogError("Animator não encontrado!");

        if (centerRb == null) Debug.LogError("centerRb não atribuído no Inspector!");
        if (gun == null) Debug.LogError("Gun não atribuído no Inspector!");
    }


    private void Update()
    {

        if (!isDashing)
        {
            moveHorizontal = Input.GetAxis("Horizontal");// Pega o input horizontal
            moveVertical = Input.GetAxis("Vertical");// Pega o input Vertical

            if (Input.GetKeyDown(KeyCode.Q) && canDash == true)
            {
                StartCoroutine(Dash());
            }
            if (Input.GetMouseButton(0))
            {
                gun.Fire();
            }
        }
            if (vida <= 0)
                logicaVida();

            switch (state)
            {
                case 0://IDLE
                    if (moveHorizontal != 0 || moveVertical != 0)
                        state = 1;
                    ChangeAnimationState(PLAYER_IDLE);
                    break;
                case 1://WALKING ON GROUND
                    ChangeAnimationState(PLAYER_RUN);

                    break;
                case 2:
                    //tanking damage
                    break;
            }

        m_Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    

    private void FixedUpdate()
    {
        Vector2 aimDirection = m_Position - centerRb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        centerRb.rotation = aimAngle;
        centerRb.position = rb.position;
        //gun_pivo_center();
        flip_player();

        switch (state)
        {
            case 0://Idle
                break;
            case 1://Walking on ground

                gun_pivo_center();
                if (groundMovement() == 0)
                    state = 0;
                break;
            case 2:
                //tanking damage
                state = 0;
                break;
        }
       
    }

    void gun_pivo_center()
    {
        Vector2 aimDirection = m_Position - centerRb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        centerRb.rotation = aimAngle;
        centerRb.position = rb.position;
    }

    void flip_player()
    {
        Vector2 aimDirection = m_Position - centerRb.position;
        Vector3 playerScale = transform.localScale;
        if (aimDirection.x < 0)
        {
            //Faz flipar
            transform.eulerAngles = new Vector3(playerScale.x, 180, playerScale.z);
        }
        else if (aimDirection.x > 0)
        {
            //Faz Flipar
            transform.eulerAngles = new Vector3(playerScale.x, 0, playerScale.z);
        }
    }

    private void logicaVida()
    {

        if (vida <= 0)
            morrer();
    }

    public void aumentaVida(float vida)
    {
        vidaTotal += vida;
        base.vida += vida;
    }
    public void aumentaShield(float shieldGanho)
    {
        shield = shieldGanho;
        shieldTotal = shieldGanho;
    }

    private float groundMovement()
    {
        if (!isDashing)
        {
            position.x = rb.position.x + moveHorizontal * speed;
            position.y = rb.position.y + moveVertical * speed;
        }
        else
        {
            position.x = rb.position.x + (moveHorizontal * speed * poderDash);
            position.y = rb.position.y + (moveVertical * speed * poderDash);
        }

        rb.position = position;
        return moveHorizontal+moveVertical;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        yield return new WaitForSeconds(tempoDash);
        isDashing = false;
        yield return new WaitForSeconds(cooldownDash);
        canDash = true;
    }

}
