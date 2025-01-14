using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject winScreen1;
    public GameObject winScreen2;
    public GameObject goal1;
    public GameObject goal2;
    public GameObject playerUI;
    private EventSystem m_EventSystem;
    private WinGame goalScript1;
    private WinGame goalScript2;


    private void Awake() {
        goalScript1 = goal1.GetComponent<WinGame>();
        goalScript2 = goal2.GetComponent<WinGame>();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        m_EventSystem = EventSystem.current;
    }

    void Update()
    {
        if (goalScript1.playerWon || goalScript2.playerWon) {
            WinGame();
        }
    }
    public void WinGame()
    {
        playerUI.SetActive(false);
        if (goalScript1.playerWon) {
            winScreen1.SetActive(true);
            m_EventSystem.SetSelectedGameObject(GameObject.Find("Restart 1"));
        } else {
            winScreen2.SetActive(true);
            m_EventSystem.SetSelectedGameObject(GameObject.Find("Restart 2"));
        }
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