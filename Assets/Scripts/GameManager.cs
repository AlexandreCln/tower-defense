using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int tmpMoney;
    public static bool GameIsOver;

    public GameObject gameOverUI;

    void Start()
    {
        // default values of static properties stay over scenes, so keep this assignation in the Start method
        GameIsOver = false;

        tmpMoney = PlayerStats.Money;
    }
    
    void Update()
    {
        if (GameIsOver)
            return;
            
        if (PlayerStats.Lives <= 0)       
        {
            EndGame();
        }
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
}
