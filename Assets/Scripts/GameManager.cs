using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager boardScript;
    public int playerFoodPoints = 100;
    [HideInInspector]
    public bool playersTurn = true;

    private int level = 3;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        this.boardScript = this.GetComponent<BoardManager>();
        InitGame();
    }

    public void GameOver()
    {
        enabled = false;
    }

    void InitGame()
    {
        boardScript.SetUpScene(level);
    }
}
