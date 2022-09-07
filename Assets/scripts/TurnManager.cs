using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    private AnimalManager _animalManager;
    [SerializeField] private GameObject _animalGO;

    public Text battleText;
    public Text turnCounterText;
    public Button attackButton;

    public int turnCounter = 1;

    public static bool playerCanMove;

    public float playerHealth;

    private void Awake()
    {
        attackButton.interactable = false;
    }

    private void Start()
    {
        _animalManager = _animalGO.GetComponent<AnimalManager>();
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

        attackButton.interactable = false;
        yield return new WaitForSeconds(2);
        _animalManager.EnemyTurn();
        turnCounter++;
        Debug.Log("Turn end");
        StartCoroutine(Turn());
    }

    private void UpdateUI()
    {
        turnCounterText.text = $"Turn {turnCounter}";
    }
}
