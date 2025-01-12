using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject winScreen;
    public Button restartButton;
    public bool Paused;
    public bool isGameActive = false;

    public void StartGame()
    {
        Time.timeScale = 1;
        isGameActive = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        // if (player.transform.position.y < -50)
        // {
        //     GameOver();
        // }
    }
    public void WinGame()
    {
        winScreen.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        isGameActive = false;
        Time.timeScale = 0;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Restart Game");
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        Debug.Log("Game Over");
    }
}