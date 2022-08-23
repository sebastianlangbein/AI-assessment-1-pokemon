using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Text _healthText; 
    [SerializeField] private float _health, _attack;
    [SerializeField] private GameObject _animalGO;
    private AnimalAi _animalAi;

    private void Start()
    {
        _animalAi = _animalGO.GetComponent<AnimalAi>();
        UpdateUI();
    }
    private void UpdateUI()
    {
        _healthText.text = _health.ToString();
    }

    public void PlayerTakeDamage()
    {
        float damage = _animalAi.EnemyAttack();
        //pass damage through blocking multipliers
        if (_health - damage < 0)
        {
            _health = 0;
        }
        else
        {
            _health -= damage;
        }
        UpdateUI();
    }

    public float PlayerAttack()
    {
        //put 
        return _attack;
    }

    public void PlayerTurn()
    {
        TurnManager.playerCanMove = false;
        _animalAi.EnemyTakeDamage();
    }
}
