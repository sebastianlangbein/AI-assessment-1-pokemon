using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalManager : MonoBehaviour
{
    [SerializeField] private Text _healthText, _stateText, _attackText, _descText;
    [SerializeField] private int _health, _maxHealth;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            _stateMachine.StateCheck();
        }
    }

    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private StateMachine _stateMachine;

    public List<string> curMoveList = new List<string>();
    //private string[] _moveList = { "high", "mid", "low" };

    public int attack;
    public int moveAmount = 3;
    public Dictionary<string, int> moveDictionary = new Dictionary<string, int>()
    {
        ["high"] = 0,
        ["mid"] = 0,
        ["low"] = 0
    };

    public bool enemyIsMoving = false;

    private void Start()
    {
        UpdateUI();
        _stateMachine.StateCheck();
    }

    private void UpdateUI()
    {
        _attackText.text = $"Attack: {attack}";
        _healthText.text = $"Health: {Health}";
        _stateText.text = $"{_stateMachine.CurrentState} Form";
    }

    public void MoveSelect()
    {
        _descText.text = "ANIMAL is charging\n";
        for (int i = 0; i < moveAmount; i++)
        {
            int moveHeightIndex = Random.Range(0, _moveList.Length);
            string move = _moveList[moveHeightIndex];
            curMoveList.Add(move);
            _descText.text += $"{curMoveList[i]}, ";
        }
        _descText.text += "\nattacks";
    }

    public void EnemyTurn()
    {
        StartCoroutine(EnemyAttack());
    }

    private IEnumerator EnemyAttack()
    {
        enemyIsMoving = true;
        for (int i = 0; i < moveAmount; i++)
        {
            _playerManager.PlayerTakeDamage(attack,curMoveList[i]);
            yield return new WaitForSeconds(1);
        }
        enemyIsMoving = false;
        _descText.text = "Recharging...";
    }

    public void EnemyTakeDamage(int playerDamageAmnt)
    {
        if (Health - playerDamageAmnt < 1)
        {
            Health = 1;
        }
        else
        {
            Health -= playerDamageAmnt;
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