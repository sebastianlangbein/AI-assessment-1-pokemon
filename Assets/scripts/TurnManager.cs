using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private AnimalManager _animalManager;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private StateMachine _stateMachine;

    public Text battleText;
    [SerializeField] private Text turnCounterText;

    private int _turnCounter = 1;

    public int TurnCounter
    {
        get { return _turnCounter; }

        set
        {
            _turnCounter = value;
        }
    }

    private void Start()
    {
        StartCoroutine(Turn());
    }

    private void TurnStart()
    {
        UpdateUI();
        _animalManager.MoveSelect();
        _playerManager.PlayerCanMove();
        battleText.text = "YOUR TURN";
    }

    private void TurnEnd()
    {
        _animalManager.curMoveList.Clear();
        _playerManager.EndTurnStuff();
        TurnCounter++;
        StartCoroutine(Turn());
    }

    private IEnumerator Turn()
    {
        TurnStart();

        while (_playerManager.playerCanMove)
        {
            yield return null;
        }

        battleText.text = "ENEMY TURN";
        yield return new WaitForSeconds(2);

        _animalManager.EnemyTurn();
        while (_animalManager.enemyIsMoving)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2);

        TurnEnd();
    }

    private void UpdateUI()
    {
        turnCounterText.text = $"Turn {TurnCounter}";
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
