using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    //Variáveis
    [Header("VarAUX")]
    public int state; 
    protected float moveHorizontal;
    protected float moveVertical;
    public bool lookingRight;

    [Header("Stats")]
    [SerializeField]public float speed;
    [SerializeField]public float life;
    [SerializeField]public float danoBase;
    [SerializeField]public float vidaTotal;

    [Header("Objetos Unity")]
    public Vector2 position;
    protected Rigidbody2D rb;
    [SerializeField] public SpriteRenderer sprite;
    

    public void ReduzirVida(float dano)
    {
        life -= dano;
    }

    protected void morrer()
    {
        //Dar overight em cada classe
        this.gameObject.SetActive(false);
    }

}
