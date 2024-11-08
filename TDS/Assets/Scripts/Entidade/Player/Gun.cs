using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Objetos Unity")]
    public GameObject bulletPreab;
    public Transform firePoint;
    [Header("Variáveis")]
    public float fireForce = 1.2f;
    public bool canFire = true;
    public bool isReloading = false;
    public int maxMag = 9;
    public int currentMag = 9;
    [SerializeField] private float time_to_reload;

    // Animation
    protected Animator _animator;
    protected string _currentState;

    // Animações
    const string GUN_FIRE = "Gunfire";
    const string GUN_IDLE = "IDLE";
    const string GUN_RELOAD = "Recharge_Gun";


    // fireDelay em segundos e fireRate em fixedGameUpdates
    public float fireDelay;
    private float fireRate;

    // Enquanto não houver animaçõesS
    public float reloadDelay;
    private float reloadSpeed;

    // Quantos fixedGameUpdates ocorreram desde um determinado evento
    private int fUpdateCount = 0;

    public void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        fireDelay = 0.6f;
        fireRate = fireDelay * 50.0f;
        reloadDelay = 1.5f;
        reloadSpeed = reloadDelay * 50.0f;
        time_to_reload = 0.9f;
    }

    public void Fire()
    {
        if (canFire == true && isReloading == false && currentMag > 0)
        {
            ChangeAnimationState(GUN_FIRE);
            GameObject bullet = Instantiate(bulletPreab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.right * fireForce, ForceMode2D.Impulse);
            canFire = false;
            StartCoroutine(ResetAnimationState());
            currentMag--;
        }
        else if (isReloading == false && currentMag == 0)
        {
            isReloading = true;
            ChangeAnimationState(GUN_RELOAD);
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(time_to_reload);
        canFire = false;
        fUpdateCount = 0;
        currentMag = 9;
        StartCoroutine(ResetAnimationState());
    }

    void FixedUpdate()
    {
        // Implementação da fire rate
        if (canFire == false && isReloading == false)
        {
            if (fUpdateCount < fireRate)
            {
                fUpdateCount++;
            }
            else
            {
                fUpdateCount = 0;
                canFire = true;
            }
        }

        // Implemetação temporária do reload
        else if (canFire == false && isReloading == true)
        {
            if (fUpdateCount < reloadSpeed)
            {
                fUpdateCount++;
            }
            else
            {
                fUpdateCount = 0;
                canFire = true;
                isReloading = false;
            }
        }
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

    private IEnumerator ResetAnimationState()
    {
        yield return new WaitForSeconds(fireDelay-0.4f); // Ajuste fireDelay ao tempo da animação
        ChangeAnimationState(GUN_IDLE); // Retorna ao estado neutro (Idle)
    }

}
