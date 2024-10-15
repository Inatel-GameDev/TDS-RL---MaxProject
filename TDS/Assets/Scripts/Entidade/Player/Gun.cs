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
        fireDelay = 0.4f;
        fireRate = fireDelay * 50.0f;
        reloadDelay = 1.5f;
        reloadSpeed = reloadDelay * 50.0f;
    }

    public void Fire()
    {
        if (canFire == true && isReloading == false && currentMag > 0)
        {
            GameObject bullet = Instantiate(bulletPreab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.right * fireForce, ForceMode2D.Impulse);
            canFire = false;
        }
        else if (isReloading == false && currentMag == 0)
        {
            Reload();
        }
    }

    public void Reload()
    {
        canFire = false;
        isReloading = true;
        fUpdateCount = 0;
    }

    void FixedUpdate()
    {
        // Implementação da fire rate
        if (canFire == false && isReloading == false)
        {
            if (fUpdateCount < fireRate)
            {
                fUpdateCount++;
                Debug.Log("To no if"+ fUpdateCount+" lol  "+ fireRate);
            }
            else
            {
                fUpdateCount = 0;
                canFire = true;
                Debug.Log("To no else" + fUpdateCount + " lol  " + fireRate);
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
}
