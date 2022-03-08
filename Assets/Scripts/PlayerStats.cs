using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 200;
    public static int Lives;
    public int startLives = 30;

    void Start()
    {
        // reset money across levels
        Money = startMoney;
        Lives = startLives;
    }
}
