using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MinionsSpawn : MonoBehaviour
{
    [Header("Objetos Unity")]
    public GameObject mobPrefab;
    [SerializeField] private GameObject[] spawnsObjects;

    [Header("Variáveis")]
    public string dificult;
    [SerializeField] float timeToSpawn;
    private float numWaves;
    private int wavesCont = 0;
    private Fase fase;
    private Fase0 fase0;
    private FaseTeste faseteste;// Referência para o script Fase0
    private GameManager game;
    private List<int> spawns = new List<int>();
    float maxEnemyes;
    float enemyesPerWave;
    public bool all_enemys_invoked;
    // Start is called before the first frame update
    void Start()
    {
        all_enemys_invoked = false;
        timeToSpawn = 8f;
        game = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        GameObject gameManagerObject = GameObject.Find("Game_Manager");
        if (gameManagerObject != null)
        {
            game = gameManagerObject.GetComponent<GameManager>();
        }
        else
        {
            Debug.LogError("Game_Manager object not found in the scene.");
            return;  // Opcionalmente, encerre o método se `game` for essencial
        }
        // Ajusta o componente `Fase` de acordo com a fase atual
        if (game.fase == "Fase0")
        {
            fase = GameObject.Find("Fase").GetComponent<Fase0>(); // Acessa Fase0
        }if (game.fase == "FaseTeste")
        {
            fase = GameObject.Find("Fase").GetComponent<FaseTeste>(); // Acessa Fase0
        }
        //else if (game.fase == "Fase1")
        //{
        //    fase = GameObject.Find("Fase").GetComponent<Fase1>(); // Acessa Fase1
        //}

        // Define `maxEnemyes` com base na dificuldade
        switch (game.dificuldade)
        {
            case GameManager.GameDificult.easy:
                maxEnemyes = fase.max_enemys_easy;
                break;
            case GameManager.GameDificult.medium:
                maxEnemyes = fase.max_enemys_medium;
                break;
            case GameManager.GameDificult.hard:
                maxEnemyes = fase.max_enemys_hard;
                break;
        }
        numWaves = fase.numWaves;
        enemyesPerWave = maxEnemyes / numWaves;
        InvokeRepeating("spawnMob", 1f, timeToSpawn);
    }

    void ConfigureSpawns()
    {
        spawns.Clear();
        int spawnsNuns = spawnsObjects.Length;
        Debug.Log(enemyesPerWave);
        for (int i = 0;i < enemyesPerWave;i++)
        {
            int chose_sapawn;
            do
                chose_sapawn = UnityEngine.Random.Range(0, spawnsNuns);
            while (!checkSpawnValidation(chose_sapawn));
            spawns.Add(chose_sapawn);
        }
    }

    bool checkSpawnValidation(int spawn_to_be_validated)
    {
        foreach(var spawn in  spawns)
        {
            if (spawn == spawn_to_be_validated)
                return false;
        }
        return true;
    }

    void spawnMob()
    {
        ConfigureSpawns();
        foreach (var mob in spawns)
        {
            Instantiate(mobPrefab, spawnsObjects[mob].transform.position, spawnsObjects[mob].transform.rotation);
        }
        wavesCont++;

        if (wavesCont >= numWaves)
        {
            CancelInvoke("spawnMob");
            all_enemys_invoked = true;
        }
    }
}
