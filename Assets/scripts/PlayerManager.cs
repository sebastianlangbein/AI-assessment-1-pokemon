using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Text _healthText, _energyText, _blockingText, _blockStrText;
    [SerializeField] private int _health, _attack, _attackRange, _blockAmount;
    [SerializeField] private GameObject _attackButtons;
    [SerializeField] private GameObject _retryButton;

    [SerializeField] private AnimalManager _animalManager;
    [SerializeField] private TurnManager _turnManager;

    public List<string> playerBlock = new List<string>();

    public bool playerCanMove;
    [SerializeField] private int _maxEnergy;
    public int energy;

    public enum BlockStrength
    {
        none = 0,
        strong = 1,
        average = 2,
        weak = 3,
    }

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
        _healthText.text = $"{_health}";
        _energyText.text = $"{energy}";
        _blockStrText.text = $"{(BlockStrength)playerBlock.Count}";
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
        energy = _maxEnergy;
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
        _blockAmount = 4 - playerBlock.Count;
        if (_blockAmount >= 4 || _blockAmount < 0)
        {
            _blockAmount = 0;
        }

        int blockedDamage = Mathf.Max(damage - _blockAmount, 0);
        if (playerBlock.Contains(enemyMoveHeight))
        {
            if (_health - blockedDamage < 0)
            {
                _health = 0;
                _turnManager.battleText.text = "YOU DIED";
                _retryButton.SetActive(true);
                Death();
            }
            else
            {
                _health -= blockedDamage;
                _turnManager.battleText.text = $"BLOCKED THE ATTACK AND TOOK {blockedDamage} DAMAGE";
            }
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
        int randAttack = Random.Range(_attack, _attack + (_attackRange + 1));
        _animalManager.EnemyTakeDamage(randAttack);
        //_animalManager.EnemyTakeDamage(_attack);
    }
}
