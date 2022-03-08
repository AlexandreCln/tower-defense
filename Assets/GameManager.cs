using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int tmpMoney;
    private bool gameEnded = false;

    void Start()
    {
        tmpMoney = PlayerStats.Money;
    }
    void Update()
    {
        if (gameEnded)
            return;
            
        if (PlayerStats.Lives <= 0)       
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
    }
}
