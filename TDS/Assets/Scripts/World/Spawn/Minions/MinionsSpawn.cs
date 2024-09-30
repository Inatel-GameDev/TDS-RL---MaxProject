using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionsSpawn : MonoBehaviour
{
    [Header("Objetos Unity")]
    public GameObject mobPrefab;
    [SerializeField] private GameObject[] spawnsObjects;

    [Header("Variáveis")]
    public string dificult;
    public string fase; // Vou mecher com esse pra frente
    [SerializeField] float timeToSpawn;
    [SerializeField] private int numWaves;
    private int wavesCont = 0;

    private List<int> spawns = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        ConfigureSpawns();
        InvokeRepeating("spawnMob", 0.1f, timeToSpawn);
    }

    void ConfigureSpawns()
    {
        spawns.Clear();

        int randomNumber1, randomNumber2, randomNumber3, randomNumber4;
        int spawnsNuns = spawnsObjects.Length;

        switch (dificult)
        {
            case "Easy":
                randomNumber1 = UnityEngine.Random.Range(0, spawnsNuns);
                do
                {
                    randomNumber2 = UnityEngine.Random.Range(0, spawnsNuns);
                } while (randomNumber2 == randomNumber1);

                spawns.Add(randomNumber1);
                spawns.Add(randomNumber2);
                break;

            case "Medium":
                randomNumber1 = UnityEngine.Random.Range(0, spawnsNuns);
                do
                {
                    randomNumber2 = UnityEngine.Random.Range(0, spawnsNuns);
                } while (randomNumber2 == randomNumber1);
                do
                {
                    randomNumber3 = UnityEngine.Random.Range(0, spawnsNuns);
                } while (randomNumber3 == randomNumber1 || randomNumber3 == randomNumber2);

                spawns.Add(randomNumber1);
                spawns.Add(randomNumber2);
                spawns.Add(randomNumber3);
                break;

            case "Hard":
                randomNumber1 = UnityEngine.Random.Range(0, spawnsNuns);
                do
                {
                    randomNumber2 = UnityEngine.Random.Range(0, spawnsNuns);
                } while (randomNumber2 == randomNumber1);
                do
                {
                    randomNumber3 = UnityEngine.Random.Range(0, spawnsNuns);
                } while (randomNumber3 == randomNumber1 || randomNumber3 == randomNumber2);
                do
                {
                    randomNumber4 = UnityEngine.Random.Range(0, spawnsNuns);
                } while (randomNumber4 == randomNumber1 || randomNumber4 == randomNumber2 || randomNumber4 == randomNumber3);

                spawns.Add(randomNumber1);
                spawns.Add(randomNumber2);
                spawns.Add(randomNumber3);
                spawns.Add(randomNumber4);
                break;
        }
    }

    void spawnMob()
    {
        foreach (var mob in spawns)
        {
            Instantiate(mobPrefab, spawnsObjects[mob].transform.position, spawnsObjects[mob].transform.rotation);
        }
        wavesCont++;

        if (wavesCont >= numWaves)
        {
            CancelInvoke("spawnMob");
        }
    }
}
