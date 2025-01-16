//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : Player
{
    [Header("Objetos Unity")]
    public Gun gun;
    public Rigidbody2D centerRb;

    [Header("Vari�veis")]
    Vector2 m_Position;
    private bool canDash = true;
    private bool isDashing = false;

    [Header("Dash")]
    public float tempoDash = 0.5f;
    public float poderDash = 3f;
    public float cooldownDash = 4.0f;
    private float dashCDCurrent = 4.5f;
    public Slider dashSlider;

    //Constantes anima��o
    const string PLAYER_IDLE = "PlayerIdle";
    const string PLAYER_RUN = "Player_Run";
    const string PLAYER_JUMP = "Player_Jump";
    const string PLAYER_FALL = "Player_Fall";
    const string PLAYER_DAMAGE = "Player_damage";
    const string PLAYER_ATTACK = "Player_Attack";
    const string PLAYER_DASH = "Player_Dash";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) Debug.LogError("Rigidbody2D n�o encontrado!");

        sprite = GetComponent<SpriteRenderer>();
        if (sprite == null) Debug.LogError("SpriteRenderer n�o encontrado!");

        _animator = gameObject.GetComponent<Animator>();
        if (_animator == null) Debug.LogError("Animator n�o encontrado!");

        if (centerRb == null) Debug.LogError("centerRb n�o atribu�do no Inspector!");
        if (gun == null) Debug.LogError("Gun n�o atribu�do no Inspector!");
    }


    private void Update()
    {

        if (!isDashing)
        {
            moveHorizontal = Input.GetAxis("Horizontal");// Pega o input horizontal
            moveVertical = Input.GetAxis("Vertical");// Pega o input Vertical
            if (Input.GetKeyDown(KeyCode.Q) && canDash == true)
            {
                state = 3;
                canDash = false;
                ChangeAnimationState(PLAYER_DASH);
                StartCoroutine(Dash());
                dashCDCurrent = 0.0f;
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
                case 3:
                //They see me roling
                if (!isDashing)
                    state = 0;
                    break;
            }

        m_Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Barra de cooldown do dash

        dashCDCurrent += Time.deltaTime;
        dashCDCurrent = Mathf.Clamp(dashCDCurrent, 0, (cooldownDash+tempoDash));
        dashSlider.value = dashCDCurrent / (cooldownDash + tempoDash);

        if(dashSlider.value >= 1)
        {
            dashSlider.gameObject.SetActive(false);
        }
        else
        {
            dashSlider.gameObject.SetActive(true);
        }
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
            case 3:
                gun_pivo_center();
                if (groundMovement() == 0)
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
        return Mathf.Abs(moveHorizontal)+ Mathf.Abs(moveVertical);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        yield return new WaitForSeconds(tempoDash);
        ChangeAnimationState(PLAYER_RUN);
        isDashing = false;
        yield return new WaitForSeconds(cooldownDash);
        canDash = true;
    }

}
