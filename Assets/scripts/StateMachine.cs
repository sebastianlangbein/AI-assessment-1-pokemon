using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateMachine : MonoBehaviour
{
    //Possible states
    public enum State
    {
        Croc = 0,
        Monkey = 1,
        Dog = 2,
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
            //increase divisor to increase cycle repeats
            //increase modulus to increase cycle size
            //minus 1 for consistent cycle repeats
            CurrentState = (State)((_animalManager.Health - 1) / 10 % 3);
        }
    }
    /*
       stats moves def low mid high 
    form
    Dog       3     1   3   1   3
    Monk      2     2   1   3   1
    Croc      4     2   2   2   1
     */
    private IEnumerator DogState()
    {
        _stateDescText.text = "DOG FORM\n" +
            "It has strong high and low attacks, but a weak mid attack." +
            "\nNext form: Monkey";

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
            "It has a strong mid attack, but weak high and low attacks." +
            "\nNext form: Croc";

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
               "It has average mid and low attacks, and a weak high attack." +
               "\nNext form: Dog";

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
