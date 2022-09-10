using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Text _healthText; 
    [SerializeField] private int _health, _attack;

    [SerializeField] private AnimalManager _animalManager;

    public List<string> playerBlock = new List<string>();

    public bool playerCanMove;
    public int energy;

    private void Awake()
    {
        playerCanMove = false;
    }

    private void Start()
    {
        UpdateUI();
    }

    public void SetBlock(string blockHeight)
    {
        playerBlock.Add(blockHeight);
        foreach (string block in playerBlock)
        {
            print(block);
        }
    }

    private void UpdateUI()
    {
        _healthText.text = $"Health: {_health}";
    }

    public void OnMoveEnergyDown()
    {
        energy--;
        if (energy <= 0)
        {
            PlayerCannotMove();
        }
        print(energy);
    }

    public void PlayerCanMove()
    {
        energy = 3;
        playerCanMove = true;
    }

    public void PlayerCannotMove()
    {
        playerCanMove = false;
    }

    public void PlayerTakeDamage(int damage, string enemyMoveHeight)
    {
        if (playerBlock.Contains(enemyMoveHeight))
        {
            print("blocked the attack");
        }
        else
        {
            if (_health - damage < 0)
            {
                _health = 0;
                print("dead");
            }
            else
            {
                _health -= damage;
                print($"took {damage} damage");
            }
        }
        UpdateUI();
    }

    public void PlayerTurn()
    {
        _animalManager.EnemyTakeDamage(_attack);
    }
}
