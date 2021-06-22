using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public struct Score
{
    public int enemyKill;
    public int current;
    public int high;
}

public enum GameState { Idle, Playing, Pause, Over }


public class GameManager : MonoBehaviour
{
    [Header("Reference UI")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;

    public static GameManager Instance;

    private GameState gameState;
    private Score score;
    private int level;

    private void Awake()
    {
        Instance = this;
        GameObject.DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartGame();
    }

    void Update()
    {

    }

    #region GameState Methods

    public void StartGame()
    {
        SetStateGame(GameState.Playing);
        Time.timeScale = 1;

        ResetLevel();
        ResetScore();
    }

    public void PauseGame()
    {
        SetStateGame(GameState.Pause);
        Time.timeScale = 0;

    }

    public void ContinueGame()
    {
        SetStateGame(GameState.Playing);
        Time.timeScale = 1;

    }

    public void OverGame()
    {
        SetStateGame(GameState.Over);
        Time.timeScale = 1;

        AllEnemyPauseMove();
    }


    public bool IsPlaying()
    {
        bool isPlaying = gameState == GameState.Playing;

        return isPlaying;
    }

    public bool IsIdle()
    {
        bool isIdle = gameState == GameState.Idle;

        return isIdle;
    }

    public bool IsPause()
    {
        bool isPause = gameState == GameState.Pause;

        return isPause;
    }

    public bool IsOver()
    {
        bool isOver = gameState == GameState.Over;

        return isOver;
    }


    public bool IsActive()
    {
        switch (gameState)
        {
            case GameState.Playing:
                {
                    return true;
                }
            default:
                {
                    return false;
                }
        }
    }


    public GameState GetStateGame()
    {
        return gameState;
    }

    public void SetStateGame(GameState state)
    {
        gameState = state;
    }

    #endregion

    #region Level & Score Methods

    public void NextLevel()
    {

    }

    public void UpdateScore(int scoreAdd)
    {
        score.enemyKill++;
        score.current += scoreAdd;
        score.high = score.current > score.high ? score.current : score.high;

        // update score text
        scoreText.text = string.Format("Score: {0}", score.current);
        highScoreText.text = string.Format("High Score: {0}", score.high);
    }


    public void ResetLevel()
    {
        level = 1;
    }

    public void ResetScore()
    {
        score.enemyKill = 0;
        score.current = 0;
    }

    #endregion

    #region Ball

    public int GetCountAllEnemy()
    {
        EnemyController[] allEnemy = FindObjectsOfType<EnemyController>();

        return allEnemy.Length;
    }

    public void AllEnemyPauseMove()
    {
        EnemyController[] allEnemy = FindObjectsOfType<EnemyController>();

        foreach (EnemyController enemy in allEnemy)
        {
            enemy.isCanMove = false;
        }
    }

    #endregion

}
