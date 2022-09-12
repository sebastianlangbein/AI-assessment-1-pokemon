using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour
{
    //Possible states
    public enum State
    {
        Monkey = 0,
        Dog = 1,
        Croc = 2,
        ultInst = 3
    }

    //Ai's current state
    [SerializeField] private State _currentState;
    [SerializeField] private Text _stateDescText;

    //Current state property
    public State CurrentState
    {
        get { return _currentState; }
        set { _currentState = value; }
    }

    [SerializeField] private AnimalManager _animalManager;

    private void Start()
    {
        StateCheck();
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
            case State.ultInst:
                StartCoroutine(ultInst());
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
            //go into ultra instinct state at 1 health
            CurrentState = State.ultInst;
        }
        else
        {
            //health     0 1 2 3 4 5 6 7 8 9
            //health/2   0 0 1 1 2 2 3 3 4 4
            //health/2%2 0 0 1 1 0 0 1 1 0 0
            //increase divisor in increase cycle repeats
            //increase modulus to increase cycle size
            //minus 1 for consistent cycle repeats
            CurrentState = (State)((_animalManager.Health - 1) / 4 % 3);
        }
    }

    private IEnumerator DogState()
    {
        _stateDescText.text = "DOG FORM\n" +
            "A tenacious and head-strong form, " +
            "it favours attacking moves but has low defence. " +
            "It has strong mid and low attacks, but a weak high attack.";
        //set to correct stats
        _animalManager.DogStats();
        while (CurrentState == State.Dog)
        {
            yield return null;
        }
        NextState();
    }

    private IEnumerator MonkeyState()
    {
        _stateDescText.text = "MONKEY FORM\n" +
            "A whimsical and carefree form, " +
            "it favours healing moves and has high defence. " +
            "It has a strong high attack, but weak mid and low attacks.";

        _animalManager.MonkeyStats();
        while (CurrentState == State.Monkey)
        {
            yield return null;
        }
        NextState();
    }

    private IEnumerator CrocState()
    {
        _stateDescText.text = "CROC FORM\n" +
               "The most powerful form. An unrelenting attacker and powerful defender, " +
               "it favours attacking and healing moves, and has very high defence. " +
               "It has strong high, mid and low attacks.";
        print("croc stats change");
        _animalManager.CrocStats();
        while (CurrentState == State.Croc)
        {
            yield return null;
        }
        NextState();
    }

    private IEnumerator ultInst()
    {
        _stateDescText.text = "???";

        _animalManager.UltInstStats();
        while (CurrentState == State.ultInst)
        {
            yield return null;
        }
        NextState();
    }
}
