using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AnimalManager : MonoBehaviour
{
    [SerializeField] private Text _healthText, _stateText, _defenceText, _descText;
    [SerializeField] private int _health, _maxHealth;
    [SerializeField] private Sprite _dogImg, _monkeyImg, _crocImg, _chickenImg;
    public bool statsUpdated = false;
    private int moveSelectAmount;


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

    private RectTransform _rt;
    private Image _sprite;

    private int _defence;
    public int moveAmount = 3;

    public List<string> curMoveList = new List<string>();
    public Dictionary<string, int> moveDict = new Dictionary<string, int>()
    {
        ["high"] = 0,
        ["mid"] = 0,
        ["low"] = 0
    };

    public bool enemyIsMoving = false;

    private void Start()
    {
        _rt = GetComponent<RectTransform>();
        _sprite = GetComponent<Image>();
        UpdateUI();
    }

    private void UpdateUI()
    {
        _defenceText.text = $"Defence: {_defence}";
        _healthText.text = $"Health: {Health}";
        _stateText.text = $"{_stateMachine.CurrentState} Form";
    }

    public void MoveSelect()
    {
        moveSelectAmount = moveAmount;
        //random dictionary element key
        //int dictIndex = Random.Range(0, moveDict.Count);
        //moveDict.ElementAt(dictIndex).Key;

        _descText.text = "ANIMAL is charging\n";
        for (int i = 0; i < moveAmount; i++)
        {
            int dictIndex = Random.Range(0, moveDict.Count);
            string move = moveDict.ElementAt(dictIndex).Key;
            curMoveList.Add(move);
            _descText.text += $"{curMoveList[i]}, ";
        }
        //remove last comma
        _descText.text = _descText.text.Remove(_descText.text.Length-2,1);
        _descText.text += "attacks";
    }

    public void EnemyTurn()
    {
        StartCoroutine(EnemyAttack());
    }

    private IEnumerator EnemyAttack()
    {
        enemyIsMoving = true;
        for (int i = 0; i < moveSelectAmount; i++)
        {
            int attackDmg = moveDict[curMoveList[i]];
            _playerManager.PlayerTakeDamage(attackDmg, curMoveList[i]);
            yield return new WaitForSeconds(2/(float)moveSelectAmount);
        }
        enemyIsMoving = false;
        _descText.text = "Recharging...";
    }

    public void EnemyTakeDamage(int playerDamageAmnt)
    {
        int trueDamage = Mathf.Max(playerDamageAmnt - _defence,0);
        if (Health - trueDamage < 1)
        {
            Health = 1;
        }
        else
        {
            Health -= trueDamage;
        }
        UpdateUI();
    }

    public void DogStats()
    {
        _defence = 1;
        moveAmount = 3;
        moveDict["high"] = 3;
        moveDict["mid"] = 1;
        moveDict["low"] = 3;
        _sprite.sprite = _dogImg;
        _rt.sizeDelta = new Vector2(129, 157);
        UpdateUI();
        statsUpdated = true;
    }

    public void MonkeyStats()
    {
        _defence = 2;
        moveAmount = 2;
        moveDict["high"] = 1;
        moveDict["mid"] = 3;
        moveDict["low"] = 1;
        _sprite.sprite = _monkeyImg;
        _rt.sizeDelta = new Vector2(174, 128);
        UpdateUI();
        statsUpdated = true;
    }

    public void CrocStats()
    {
        _defence = 2;
        moveAmount = 4;
        moveDict["high"] = 1;
        moveDict["mid"] = 2;
        moveDict["low"] = 2;
        _sprite.sprite = _crocImg;
        _rt.sizeDelta = new Vector2(129, 128);
        UpdateUI();
        statsUpdated = true;
    }

    public void UltInstStats()
    {
        _defence = 99;
        moveAmount = 10;
        moveDict["high"] = 2;
        moveDict["mid"] = 2;
        moveDict["low"] = 2;
        _sprite.sprite = _chickenImg;
        _rt.sizeDelta = new Vector2(128, 145);
        UpdateUI();
        statsUpdated = true;
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