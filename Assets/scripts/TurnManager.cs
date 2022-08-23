using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public Text battleText;
    public Text turnCounterText;
    public Button attackButton;

    public static int turnCounter = 1;

    public bool playerCanMove;
    public bool enemyAlive = true;

    public float enemyHealth;
    public float enemyDamage;

    public float playerHealth;

    private void Awake()
    {
        attackButton.interactable = false;
    }

    private void Start()
    {
        StartCoroutine(Turn());
    }

    private IEnumerator Turn()
    {
        Debug.Log($"Turn {turnCounter} start");
        UpdateUI();
        playerCanMove = true;
        attackButton.interactable = true;

        while (playerCanMove)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2);
        EnemyAttack();
        turnCounter++;
        Debug.Log("Turn end");
        StartCoroutine(Turn());
    }

    private void UpdateUI()
    {
        turnCounterText.text = $"Turn {turnCounter}";
    }

    private void EnemyAttack()
    {
        if (playerHealth - enemyDamage < 0)
        {
            playerHealth = 0;
        }
        else
        {
            playerHealth -= enemyDamage;
        }
        battleText.text = $"Enemy attacks for {enemyDamage}";
    }

    public void PlayerAttack(float playerDamageAmnt)
    {
        attackButton.interactable = false;
        playerCanMove = false;

        if (enemyHealth - playerDamageAmnt < 0)
        {
            enemyHealth = 1;
        }
        else
        {
            enemyHealth -= playerDamageAmnt;
        }
        battleText.text = $"Player attacks for {playerDamageAmnt}";
    }
}
