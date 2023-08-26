using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public Text LevelStatus;
    public Text scoreText;
    public GameObject LevelDailog;

    public static UIHandler instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void ShowLevelDailog(string status, string scores)
    {
        GetComponent<starsHandle>().starsAchieved();

        LevelDailog.SetActive(true);
        LevelStatus.text = status;
        scoreText.text = scores;
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }
    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
