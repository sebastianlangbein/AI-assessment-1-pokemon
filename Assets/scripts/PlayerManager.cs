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
    private string playerBlock;
    private void Start()
    {
        _animalManager = _animalGO.GetComponent<AnimalManager>();
        UpdateUI();
    }
    public void SetBlock(string blockHeight)
    {
        playerBlock = blockHeight;
    }
    private void UpdateUI()
    {
        _healthText.text = _health.ToString();
    }

    public void PlayerTakeDamage()
    {
        float damage = _animalManager.EnemyAttack();
        string enemyMoveHeight = _animalManager.MoveSelect();
        if (enemyMoveHeight == playerBlock)
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
        UpdateUI();
    }

    public void PlayerTurn()
    {

        TurnManager.playerCanMove = false;
        playerBlock = null;
        _animalManager.EnemyTakeDamage();
    }

    public float PlayerAttack()
    {
        //different attacks return different values
        return _attack;
    }
}
