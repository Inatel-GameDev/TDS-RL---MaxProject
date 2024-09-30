using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Objetos Unity")]
    public GameObject bulletPreab;
    public Transform firePoint;
    [Header("Variáveis")]
    public float fireForce = 20f;

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPreab, firePoint.position,firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.right * fireForce,ForceMode2D.Impulse);
    }

}
