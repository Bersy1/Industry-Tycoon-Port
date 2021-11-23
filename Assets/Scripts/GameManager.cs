using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int woodCollected;
    public TextMeshProUGUI woodText;
    public int woodGoal = 3;
    private void Awake()
    {
        instance = this;
    }
    private void FixedUpdate()
    {
        if (woodText != null)
        {
            woodText.text = "<sprite=4> " + woodCollected.ToString() + "/" + woodGoal; ;
        }
        else return;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
