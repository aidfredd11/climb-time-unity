using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;

    private bool gameStarted = false;
    private bool gameOver = false;

    public bool GameStarted
    {
        get { return gameStarted; }
        set { gameStarted = value; }
    }
    public bool GameOver
    {
        get { return gameOver; }
        set { gameOver = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
