using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;

    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    private void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if(numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            playerLives--;
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentIndex);
            livesText.text = playerLives.ToString();
        }
        else
        {
            SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }
}
