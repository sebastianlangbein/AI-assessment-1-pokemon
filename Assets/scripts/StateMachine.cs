using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimalAi))]
public class StateMachine : MonoBehaviour
{
    public enum State
    {
        Attack,
        Tank,
        UltraInstinct
    }

    //Ai's current state
    [SerializeField] private State _state;

    private AnimalAi _animalAi;

    private void Start()
    {
        _animalAi = GetComponent<AnimalAi>();

        NextState();
    }

    private void NextState()
    {
        switch (_state)
        {
            case State.Attack:
                StartCoroutine(AttackState());
                break;
            case State.Tank:
                StartCoroutine(TankState());
                break;
            case State.UltraInstinct:
                StartCoroutine(UIState());
                break;
            default:
                Debug.LogWarning("_state doesn't exist within nextState function");
                break;
        }
    }

    private IEnumerator AttackState()
    {
        Debug.Log("Attack: Enter");
        while (_state == State.Attack)
        {
            //some yield is required for a coroutine
            //wait a single frame
            yield return null;
        }
        NextState();
    }

    private IEnumerator TankState()
    {
        yield return null;
    }

    private IEnumerator UIState()
    {
        yield return null;
    }
}
