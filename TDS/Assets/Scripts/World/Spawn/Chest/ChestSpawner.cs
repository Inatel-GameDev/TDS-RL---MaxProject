using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSpawner : MonoBehaviour
{

    // Serve para instanciar baús
    [SerializeField] private GameObject[] bauSpawnpointObjects;

    [Header("Objeto Unity")]
    public GameObject bauObject;

    [Header("Variáveis")]
    // Máximo de baús a serem spawnados.
    public int bauMax;
    // Mínimo de baús a seres spawnados.
    public int bauMin;
    // Número de spawnpoints de baú na cena

    // Quantos baús devem ser spawnados, deixe um número negativo para randomizar entre mínimo e máximo.
    public int bauTotal;

    // Qual dos spawnpoints foi selecionado para o spawn, por padrão o spawn 0.
    private int bauTarget = 0;

    // Quantos possíveis spawns existem.
    private int bauSpawnpointCount;

    // Quantos baús já foram spawnados
   private int bauCount = 0;



    public ChestSpawner(int bauMin, int bauMax, int bauTotal)
    {
        this.bauMin = bauMin;
        this.bauMax = bauMax;
        this.bauTotal = bauTotal;
    }


    // Start is called before the first frame update
    void Start()
    {
        bauSpawnpointCount = bauSpawnpointObjects.Length;
        Spawn();
    }

    public void Spawn()
    {
        // Checa se o valor de baús procurado é valido, se não for, gera um valor adequado
        if (bauTotal < 0)
        {
            bauTotal = Random.Range(bauMin, bauMax + 1);
        }
        if ((bauTotal-bauCount) >= (bauSpawnpointCount-bauCount))
        {
            bauTarget = 0;
            while (bauCount < bauSpawnpointCount)
            {
                if (bauSpawnpointObjects[bauTarget].activeSelf)
                {
                    Instantiate(bauObject, bauSpawnpointObjects[bauTarget].transform.position, bauSpawnpointObjects[bauTarget].transform.rotation);
                    bauSpawnpointObjects[bauTarget].SetActive(false);
                    bauCount++;
                }
                bauTarget++;
            }
        }

        else
        {
            // Código que spawna os baús, ele desativa pontos de spawn já visitados, tenta infinitamente até spawnar bauTotal baús
            while (bauCount < bauTotal)
            {
                bauTarget = Random.Range(0, bauSpawnpointCount);
                if (bauSpawnpointObjects[bauTarget].activeSelf)
                {
                    Instantiate(bauObject, bauSpawnpointObjects[bauTarget].transform.position, bauSpawnpointObjects[bauTarget].transform.rotation);
                    bauSpawnpointObjects[bauTarget].SetActive(false);
                    bauCount++;
                }
            }
        }
    }

    public int getBauCount()
    {
        return bauCount;
    }

    public void setBauCount(int newBauCount)
    {
        bauCount = newBauCount;
    }

    /* Update foi comentado para usos futuros.
    void Update()
    {
        
    }*/
}
