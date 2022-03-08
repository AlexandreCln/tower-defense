using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    public Text moneyText;
    public int smoothUpdateMultiplicator = 10;
    private int amountDisplayed;

    void Update()
    {
        if (PlayerStats.Money < amountDisplayed)
        {
            amountDisplayed -= smoothUpdateMultiplicator;
        }
        else if (PlayerStats.Money > amountDisplayed)
        {
            amountDisplayed += smoothUpdateMultiplicator;
        }
        
        moneyText.text = "$ " + amountDisplayed.ToString();
    }
}
