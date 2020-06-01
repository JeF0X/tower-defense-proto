using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum State { Start, Game, End, Exit, Pause}

public class LevelManager : MonoBehaviour
{
    State currentState;
    public bool isInBulldozeMode = false;

    private static LevelManager _instance;

    public static LevelManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        Time.timeScale = 1;
        currentState = State.Start;
    }

    private void Update()
    {
        if (currentState == State.Game && Input.GetKeyDown(KeyCode.Escape))
        {
            SetState(State.Pause);
            Time.timeScale = 0;
        }
        else if (currentState == State.Pause && Input.GetKeyDown(KeyCode.Escape))
        {
            SetState(State.Game);
            Time.timeScale = 1;
        }


    }

    public void SetState(State state)
    {
        currentState = state;
    }

    public State GetState()
    {
        return currentState;
    }

    public void ToggleBulldozeMode(bool bulldozeMode)
    {
        isInBulldozeMode = bulldozeMode;
    }
}
