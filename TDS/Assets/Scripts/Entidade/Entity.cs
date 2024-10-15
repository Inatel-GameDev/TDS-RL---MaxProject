using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    //Vari�veis
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
    [SerializeField]public float shield;
    [SerializeField]public float shieldTotal;

    [Header("Objetos Unity")]
    public Vector2 position;
    protected Rigidbody2D rb;
    [SerializeField] public SpriteRenderer sprite;


    public void ReduzirVida(float dano)
    {
        // Se o dano � menor ou igual ao escudo, apenas reduz o escudo
        if (dano <= shield)
        {
            shield -= dano;
        }
        else
        {
            // Caso o dano seja maior que o escudo, calcula o dano que sobraria para a vida
            float damageTaken = dano - shield;
            shield = 0;  // Escudo � reduzido a zero
            life -= damageTaken;  // Aplica o dano restante na vida
        }

        // Verifica se a vida foi reduzida a zero ou menos, e chama o m�todo de morte
        if (life <= 0)
        {
            morrer();
        }
    }


    protected void morrer()
    {
        //Dar overight em cada classe
        this.gameObject.SetActive(false);
    }

}
