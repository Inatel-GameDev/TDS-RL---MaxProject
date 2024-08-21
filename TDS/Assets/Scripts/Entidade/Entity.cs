using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    protected Rigidbody2D rb;
    protected int state;
    protected float moveHorizontal;
    protected float moveVertical;
    public float speed;
    public Vector2 position;
    [SerializeField] public float life;
    public float danoBase;

    public void ReduzirVida(float dano)
    {
        life -= dano;
    }

    protected void morrer()
    {
        this.gameObject.SetActive(false);
    }

}
