using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Bau : Interact
{

    [SerializeField] public GameObject[] comom_intens;
    [SerializeField] public GameObject[] epic_intens;
    [SerializeField] public GameObject[] legendary_intens;
    private int _iten_raritity;
    private int _comon_iten_picked;
    private int _epic_iten_picked;
    private int _legendary_iten_picked;
    protected Animator _animator;
    protected string _currentState;
    public AudioSource _open_chest_audioSource;
    public AudioClip _audioClip;
    [SerializeField] private ChestSpawner bau_spawn_src;

    // Animações
    const string CHEST_SPAWN = "ChestSpawn";
    const string CHEST_OPEN = "ChestOpen";

    

    private void Awake()
    {
        _animator = gameObject.GetComponent<Animator>();
        ChangeAnimationState(CHEST_SPAWN);
        bau_spawn_src = GameObject.Find("ChestSpawnpoints").GetComponent<ChestSpawner>();
    }

    public override void Interacting()
    {
        _open_chest_audioSource.clip = _audioClip;
        _open_chest_audioSource.Play();
        
        its_destroying = true;
        _iten_raritity = Random.Range(0, 100);
        _comon_iten_picked = Random.Range(0, comom_intens.Length);
        // Descomentar
        //_epic_iten_picked = Random.Range(0, epic_intens.Length);
        //_legendary_iten_picked = Random.Range(0, legendary_intens.Length);
        // Tirar
        _iten_raritity = 50;

        ChangeAnimationState(CHEST_OPEN);

        bau_spawn_src.setBauCount(bau_spawn_src.getBauCount()-1);

        if (_iten_raritity<=70)
        {
            GameObject item = Instantiate(comom_intens[_comon_iten_picked], this.transform.position, Quaternion.identity);
        }
        else if(_iten_raritity>70 && 90 <= _iten_raritity)
        {
            Instantiate(epic_intens[_epic_iten_picked]);
        }
        else if(90 > _iten_raritity)
        {
            Instantiate(legendary_intens[_legendary_iten_picked]);
        }
        // Colocar aqui animação

        // fim animação
        //this.gameObject.SetActive(false);
    }

    protected void ChangeAnimationState(string newState)
    {
        if (newState == _currentState)
        {
            return;
        }

        _animator.Play(newState);
        _currentState = newState;
    }

}
