using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Text _healthText, _energyText, _blockingText;
    [SerializeField] private int _health, _attack;
    [SerializeField] private GameObject _attackButtons;
    [SerializeField] private GameObject _retryButton;

    [SerializeField] private AnimalManager _animalManager;
    [SerializeField] private TurnManager _turnManager;

    public List<string> playerBlock = new List<string>();

    public bool playerCanMove;
    public int energy;

    private void Awake()
    {
        playerCanMove = false;
        _retryButton.SetActive(false);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void SetBlock(string blockHeight)
    {
        playerBlock.Add(blockHeight);
        if (!_blockingText.text.Contains(blockHeight))
        {
            _blockingText.text += $"{blockHeight} ";
        }
        else
        {
            energy++;
        }
    }

    private void UpdateUI()
    {
        _healthText.text = $"Health {_health}";
        _energyText.text = $"Energy {energy}";
    }

    public void OnMoveEnergyDown()
    {
        energy--;
        UpdateUI();
        if (energy <= 0)
        {
            PlayerCannotMove();
        }
    }

    public void PlayerCanMove()
    {
        energy = 3;
        UpdateUI();
        playerCanMove = true;
        _attackButtons.SetActive(true);
    }

    public void PlayerCannotMove()
    {
        playerCanMove = false;
        _attackButtons.SetActive(false);
    }

    public void EndTurnStuff()
    {
        playerBlock.Clear();
        _blockingText.text = "Blocking\n";
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    public void PlayerTakeDamage(int damage, string enemyMoveHeight)
    {
        if (playerBlock.Contains(enemyMoveHeight))
        {
            _turnManager.battleText.text = "BLOCKED THE ATTACK";
        }
        else
        {
            if (_health - damage < 0)
            {
                _health = 0;
                _turnManager.battleText.text = "YOU DIED";
                _retryButton.SetActive(true);
                Death();
            }
            else
            {
                _health -= damage;
                _turnManager.battleText.text = ($"YOU TOOK {damage} DAMAGE");
            }
        }
        UpdateUI();
    }

    public void PlayerTurn()
    {
        _animalManager.EnemyTakeDamage(_attack);
    }
}
