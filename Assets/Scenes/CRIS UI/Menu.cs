
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StarGame()
    {
        SceneManager.LoadScene("Game");// Escena del juego cuando le damos play
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void BackTomenu()// option cuando el juego este en pausa
    {
        SceneManager.LoadScene("Menu");
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Unpause()
    {
        Time.timeScale = 1f;
    }
    public void GameOver()
    {
        SceneManager.LoadScene("Game Over");

    }
    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}