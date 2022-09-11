using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour
{
    public enum State
    {
        Dog = 0,
        Monkey = 1,
        Croc = 2
    }

    //Ai's current state
    [SerializeField] private State _state;
    [SerializeField] private Text _stateDescText;
    public State CurrentState
    {
        get { return _state; }
        set
        {
            _state = value;
        }
    }

    private AnimalManager _animalManager;

    private void Start()
    {
        _animalManager = GetComponent<AnimalManager>();
        NextState();
    }

    private void NextState()
    {
        switch (CurrentState)
        {
            case State.Dog:
                StartCoroutine(DogState());
                break;
            case State.Monkey:
                StartCoroutine(MonkeyState());
                break;
            case State.Croc:
                StartCoroutine(CrocState());
                break;
            default:
                Debug.LogWarning("_state doesn't exist within nextState function");
                break;
        }
    }

    public void StateCheck()
    {
        if (_animalManager.Health == 1)
        {
            CurrentState = State.Croc;
        }
        else
        {
            //health     0 1 2 3 4 5 6 7 8 9 10
            //health/2   0 0 1 1 2 2 3 3 4 4 5
            //health/2%2 0 0 1 1 0 0 1 1 0 0 1
            CurrentState = (State)((_animalManager.Health - 1) / 2 % 2);
        }
    }

    private void DogStats()
    {
        _stateDescText.text = "DOG FORM\n" +
            "A tenacious and head-strong form, " +
            "it favours attacking moves but has low defence. " +
            "It has strong mid and low attacks, but a weak high attack.";
        _animalManager.moveDictionary["high"] = 1;
        _animalManager.moveDictionary["mid"] = 3;
        _animalManager.moveDictionary["low"] = 3;
        _animalManager.moveDictionary.Add("bite", 2);
        _animalManager.moveDictionary.Remove("heal");
    }

    private IEnumerator DogState()
    {
        DogStats();
        while (CurrentState == State.Dog)
        {
            yield return null;
        }
        NextState();
    }

    private void MonkeyStats()
    {
        _stateDescText.text = "MONKEY FORM\n" +
            "A whimsical and carefree form, " +
            "it favours healing moves and has high defence. " +
            "It has a strong high attack, but weak mid and low attacks.";
        _animalManager.moveDictionary["high"] = 3;
        _animalManager.moveDictionary["mid"] = 1;
        _animalManager.moveDictionary["low"] = 1;
        _animalManager.moveDictionary.Add("heal", 2);
        _animalManager.moveDictionary.Remove("bite");
    }

    private IEnumerator MonkeyState()
    {
        MonkeyStats();
        while (CurrentState == State.Monkey)
        {
            yield return null;
        }
        NextState();
    }

    private void CrocStats()
    {
        _stateDescText.text = "CROC FORM\n" +
            "The most powerful form. An unrelenting attacker and powerful defender, " +
            "it favours attacking and healing moves, and has very high defence. " +
            "It has strong high, mid and low attacks.";
        _animalManager.moveDictionary["high"] = 3;
        _animalManager.moveDictionary["mid"] = 3;
        _animalManager.moveDictionary["low"] = 3;
        _animalManager.moveDictionary.Add("heal", 2);
        _animalManager.moveDictionary.Add("bite", 2);
    }

    private IEnumerator CrocState()
    {
        CrocStats();
        while (CurrentState == State.Croc)
        {
            yield return null;
        }
        NextState();
    }
}
