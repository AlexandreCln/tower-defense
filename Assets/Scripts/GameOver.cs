using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;
    void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString();
    }

    public void RetryLevel()
    {
        // Get the index of current level scene
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

    public void Menu()
    {

    }
}
