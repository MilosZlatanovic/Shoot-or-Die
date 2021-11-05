using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private bool _isGameOver;
    private UIManager _uIManager;
    [SerializeField]
    private GameObject _pauseMenu;

    private void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(1); //  Game Scene 
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenu.SetActive(true);
            Time.timeScale= 0;
        }

    }
    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }


    public void GameOver()
    {
        _isGameOver = true;
    }
}
