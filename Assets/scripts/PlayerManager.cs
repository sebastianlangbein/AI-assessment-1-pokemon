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
    private string playerBlock;
    private void Start()
    {
        _animalAi = _animalGO.GetComponent<AnimalAi>();
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

    //public void Minus()
    //{
    //    int[] attack = new int[3] { 1, 1, 1 };
    //    int[] block = new int[3] { 0, 0, 0 };
    //    int[] damage = new int[3];
    //    int damageMultiplier = 2;
    //    int sum = 0;
    //    for (int i = 0; i < attack.Length; i++)
    //    {
    //        damage[i] = attack[i] - block[i];
    //        if (damage[i] < 0)
    //        {
    //            damage[i] = 0;
    //        }
    //        sum += damage[i];
    //    }
    //    print(sum * damageMultiplier);
    //}

    public void PlayerTakeDamage()
    {
        float damage = _animalAi.EnemyAttack();
        string enemyMoveHeight = _animalAi.MoveSelect();
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
        _animalAi.EnemyTakeDamage();
    }

    public float PlayerAttack()
    {
        //different attacks return different values
        return _attack;
    }
}
