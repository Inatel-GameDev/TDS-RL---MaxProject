using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    protected Animator _animator;
    protected string _currentState;
    public SqueletonSwordRework esqueleto_src;
    private GameObject playerObj;
    protected Player playerScr;
    GameObject attackColider;
    public bool is_hiting = false;
    // Animações
    const string SWORD_HITING = "Sword_Hiting";
    const string SWORD_IDLE = "Sword_Idle";
    
    

    private void Awake()
    {
        playerObj = GameObject.FindWithTag("Player");//Tava dando erro sem isso
        playerScr = playerObj.GetComponent<Player>();
        _animator = gameObject.GetComponent<Animator>();
        if (_animator == null) Debug.LogError("Animator SWORD não encontrado!");
    }
    protected void damageToPlayer(float dano)
    {
        if(esqueleto_src.inRange)
            playerObj = GameObject.FindWithTag("Player");
            playerScr = playerObj.GetComponent<Player>();
            playerScr.ReduzirVida(esqueleto_src.danoBase);
    }
    protected void ChangeAnimationState(string newState)
    {
        if (newState == _currentState)
        {
            return;
        }

        _animator.Play(newState);
        _currentState = newState;
    }

    private void Update()
    {
        int state = esqueleto_src.state;
        if (state == 2)
        {
            if (!is_hiting)
            {
                is_hiting = true;
                StartCoroutine(ataque());
            }
        }
    }


    private IEnumerator  ataque()
    {
        ChangeAnimationState(SWORD_HITING);
        yield return new WaitForSeconds(1.2f);
        ChangeAnimationState(SWORD_IDLE);
        is_hiting = false;
    }


}
