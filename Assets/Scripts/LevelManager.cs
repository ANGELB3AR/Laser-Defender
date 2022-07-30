using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;

    ScoreKeeper scoreKeeper;
    Leaderboard leaderboard;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        leaderboard = FindObjectOfType<Leaderboard>();
    }

    public void LoadGame()
    {
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay));
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return leaderboard.SubmitScoreRoutine(scoreKeeper.GetScore());
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
