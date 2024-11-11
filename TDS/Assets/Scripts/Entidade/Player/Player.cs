using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    [Header("Player Stats")]
    //Herdadas
    //float vidaTotal
    //float life
    //float speed
    //float danoBase
    public float iaHelperSize;

    override protected void morrer()
    {
        base.morrer();
        Application.Quit();
    }

}
