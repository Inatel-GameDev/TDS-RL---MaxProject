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
    [SerializeField]public float vida;
    [SerializeField]public float danoBase;
    [SerializeField]public float vidaTotal;
    [SerializeField]public float vidaRegen;
    [SerializeField]public float shield;
    [SerializeField]public float shieldTotal;
    [SerializeField] public float shieldRegen;

    [Header("Objetos Unity")]
    public Vector2 position;
    protected Rigidbody2D rb;
    [SerializeField] public SpriteRenderer sprite;


    public void ReduzirVida(float dano)
    {
        // Se o dano é menor ou igual ao escudo, apenas reduz o escudo
        if (dano <= shield)
        {
            shield -= dano;
        }
        else
        {
            // Caso o dano seja maior que o escudo, calcula o dano que sobraria para a vida
            float damageTaken = dano - shield;
            shield = 0;  // Escudo é reduzido a zero
            vida -= damageTaken;  // Aplica o dano restante na vida
        }

        // Verifica se a vida foi reduzida a zero ou menos, e chama o método de morte
        if (vida <= 0)
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
