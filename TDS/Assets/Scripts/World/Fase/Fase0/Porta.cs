using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta : Interact
{
    public override void Interacting()
    {
        SceneManager.LoadScene(2);
    }
}
