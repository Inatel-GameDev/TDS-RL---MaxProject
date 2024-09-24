using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Helper : MonoBehaviour
{
    
    [SerializeField] private GameObject Player;
    private CircleCollider2D circleCollider;

    [SerializeField] float pantQTD;
    private float pantufas;

    private void Start()
    {
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        pantQTD = Player.GetComponent<Player>().iaHelperSize;
        pantufas = (2.75f / (2.75f + pantQTD));
        circleCollider.radius = 0.75f * pantufas;

    }

}
