using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Text _healthText; 
    [SerializeField] private float _health, _attack;
    [SerializeField] private GameObject _animalGO;
    private AnimalManager _animalManager;
    private List<string> _playerBlock = new List<string>();
    private void Start()
    {
        _animalManager = _animalGO.GetComponent<AnimalManager>();
        UpdateUI();
    }
    public void SetBlock(string blockHeight)
    {
        _playerBlock.Add(blockHeight);
    }
    private void UpdateUI()
    {
        _healthText.text = _health.ToString();
    }

    public void PlayerTakeDamage()
    {
        float damage = _animalManager.EnemyAttack();
        string enemyMoveHeight = _animalManager.MoveSelect();
        if (_playerBlock.Contains(enemyMoveHeight))
        {
            //blocked the attack
        }
        else
        {
            if (_health - damage < 0)
            {
                _health = 0;
            }
            else
            {
                _health -= damage;
            }
        }
        _playerBlock.Clear();
        UpdateUI();
    }

    public void PlayerTurn()
    {

        TurnManager.playerCanMove = false;
        _animalManager.EnemyTakeDamage();
    }

    public float PlayerAttack()
    {
        //different attacks return different values
        return _attack;
    }
}
