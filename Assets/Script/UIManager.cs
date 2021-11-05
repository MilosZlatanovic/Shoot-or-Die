using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    
    public Text _bestScore;
    [SerializeField]
    private Text _livesText;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;

    private GameManager _gameManager;
    

    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("The Game Manager is NULL");
        }

        
    }
     
    public void AddScore(int playerScore, int currentScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
        _bestScore.text = "Best Score: " + currentScore;
    }

    public void CheckForScore()
    {
      /*  if (_bestScore.text > _scoreText.text)
        {

        }*/
    }

    public void UpdateLives(int currentLives)
    {
        _livesText.text = "Lives: " + currentLives.ToString();

        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ResumePlay()
    {
        Time.timeScale = 1f;
        _gameManager.ResumeGame();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
