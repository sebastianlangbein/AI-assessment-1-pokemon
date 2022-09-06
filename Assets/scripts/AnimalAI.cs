using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalAi : MonoBehaviour
{
    //Text and float fields for boss stats
    [SerializeField] private Text _healthText, _stateText, _attackText, _defenceText, _speedText, _dodgeText, _descText;
    [SerializeField] private float _health, _maxHealth, _attack, _defence, _speed, _dodgeChance;
    [SerializeField] private GameObject _turnGO, _playerGO;
    private PlayerManager _playerManager;
    private TurnManager _turnManager;
    private string[] _moveHeight = { "high", "mid", "low" };


    private void Start()
    {
        _turnManager = _turnGO.GetComponent<TurnManager>();
        _playerManager = _playerGO.GetComponent<PlayerManager>();
        UpdateUI();
    }

    private void UpdateUI()
    {
        _healthText.text = _health.ToString();
    }

    public string MoveSelect()
    {
        int move = Random.Range(0, 3);
        return _moveHeight[move];
    }

    public float EnemyAttack()
    {
        return _attack;
    }

    public void EnemyTurn()
    {
        _playerManager.PlayerTakeDamage();
    }

    public void EnemyTakeDamage()
    {
        float playerDamageAmnt = _playerManager.PlayerAttack();
        if (_health - playerDamageAmnt < 1)
        {
            _health = 1;
        }
        else
        {
            _health -= playerDamageAmnt;
        }
        UpdateUI();
    }

    //public void Heal(int healAmount)
    //{
    //    //if health is greater than max health after a heal
    //    if (health + healAmount > maxHealth)
    //    {
    //        //set to max health
    //        health = maxHealth;
    //    }
    //    else
    //    {
    //        //health equals health plus heal
    //        health += healAmount;
    //    }
    //    UpdateUI();
    //}
}