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
    public AudioSource _fire_audioSource;
    public AudioSource _recharge_audioSource;
    public AudioClip _rechargeClip;
    public AudioClip _fireClip;


    // Animation
    protected Animator _animator;
    protected string _currentState;

    // Animações
    const string GUN_FIRE = "Gunfire";
    const string GUN_IDLE = "IDLE";
    const string GUN_RELOAD = "Recharge_Gun";


    // fireDelay em segundos e fireRate em fixedGameUpdates
    public float fireDelay;

    // Enquanto não houver animaçõesS
    public float reloadDelay;
    private float reloadSpeed;

    // Quantos fixedGameUpdates ocorreram desde um determinado evento
    private int fUpdateCount = 0;

    public void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        fireDelay = 0.6f;
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
            _fire_audioSource.clip = _fireClip;
            _fire_audioSource.Play();
            currentMag--;
            StartCoroutine(FireRate());
        }
        else if (isReloading == false && currentMag == 0)
        {
            isReloading = true;
            canFire = false;
            ChangeAnimationState(GUN_RELOAD);
        }
    }

    private void Reload()
    {
        _recharge_audioSource.clip = _rechargeClip;
        _recharge_audioSource.Play();
        isReloading = false;
        fUpdateCount = 0;
        currentMag = 9;
        ResetAnimationState();
    }

    void FixedUpdate()
    {
        // Implemetação temporária do reload
        if (canFire == false && isReloading == true)
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

    private IEnumerator FireRate()
    {
        canFire = false;
        yield return new WaitForSeconds(fireDelay);
        canFire = true;
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

    public void ResetAnimationState()
    {
        ChangeAnimationState(GUN_IDLE); // Retorna ao estado neutro (Idle)
    }
}
