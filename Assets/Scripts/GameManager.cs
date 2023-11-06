using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    private void Awake()
    {
        if(gameManager== null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public enum GameState
    {
        Idle,
        InGame,
        GameOver,
        Menu,
        Pause,
        Victory,
        Load
    }

    public GameState gameState = GameState.Idle;

    public enum InputMetod
    {
        Mouse,
        Keyboard,
        Joystick
    }

    public InputMetod inputMetod = InputMetod.Keyboard;


    


    public void GameStart()
    {
        gameState = GameState.InGame;
    }

    public void GameOver()
    {
        gameState = GameState.GameOver;
    }

    public void GamePause()
    {
        gameState = GameState.Pause;
    }
    public void GameMenu()
    {
        gameState = GameState.Menu;
    }
    public void Victory()
    {
        gameState = GameState.Victory;
    }
    public void Load()
    {
        gameState = GameState.Load;
    }
}

