using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalManager : MonoBehaviour
{
    [SerializeField] private Text _healthText, _stateText, _attackText, _descText;
    [SerializeField] private int _health, _maxHealth, _attack;

    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private TurnManager _turnManager;

    public List<string> curMoveList = new List<string>();
    private string[] _moveList = { "high", "mid", "low" };

    private int _moveAmount = 3;
    public bool enemyIsMoving = false;

    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        _attackText.text = $"Attack: {_attack}";
        _healthText.text = $"Health: {_health}";
    }

    public void MoveSelect()
    {
        _descText.text = "ANIMAL is charging up ";
        for (int i = 0; i < _moveAmount; i++)
        {
            int moveHeightIndex = Random.Range(0, _moveList.Length);
            string move = _moveList[moveHeightIndex];
            curMoveList.Add(move);
            _descText.text += $"{curMoveList[i]}, ";
        }
        _descText.text += "attacks";
    }

    public void EnemyTurn()
    {
        StartCoroutine(EnemyAttack());
    }

    private IEnumerator EnemyAttack()
    {
        enemyIsMoving = true;
        for (int i = 0; i < _moveAmount; i++)
        {
            _playerManager.PlayerTakeDamage(_attack,curMoveList[i]);
            yield return new WaitForSeconds(1);
        }
        enemyIsMoving = false;
        _descText.text = "Recharging...";
    }

    public void EnemyTakeDamage(int playerDamageAmnt)
    {
        if (_health - playerDamageAmnt < 0)
        {
            _health = 0;
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