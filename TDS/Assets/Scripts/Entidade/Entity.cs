using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    protected Rigidbody2D rb;
    public int state;
    protected float moveHorizontal;
    protected float moveVertical;
    public float speed;
    public Vector2 position;
    [SerializeField] public float life;
    public float danoBase;
    [SerializeField] public SpriteRenderer sprite;
    public bool lookingRight;

    public void ReduzirVida(float dano)
    {
        life -= dano;
    }

    protected void morrer()
    {
        this.gameObject.SetActive(false);
    }

}
