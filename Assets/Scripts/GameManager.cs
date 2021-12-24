using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject startButton, inputButton, endPanel;

    [SerializeField]
    TMP_Text scoreText, highScoreText, highScoreEndText;

    public GameObject[] BlueGO,OrangeGO;
    public float startWait, spawnWait;
    private GameObject blue, orange;

    float[] XPosition = new float[2] { 1.2f,4f };
    bool GameOverBool;

    int score, highScore;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        startButton.SetActive(true);
        inputButton.SetActive(false);
        endPanel.SetActive(false);

        Time.timeScale = 1f;
        GameOverBool = false;
        score = 0;
        highScore = PlayerPrefs.HasKey("HighScore") ? PlayerPrefs.GetInt("HighScore") : 0;
        scoreText.text = score.ToString();
        highScoreText.text = "HighScore : " + highScore.ToString();
    }

    public void StartButtonClick()
    {
        startButton.SetActive(false);
        inputButton.SetActive(true);
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(startWait);
        while (!GameOverBool)
        {
            float xpos = XPosition[Random.Range(0, XPosition.Length)];
            Vector3 tempPos = new Vector3(xpos, 10f, 0);
            GameObject tempObject = OrangeGO[Random.Range(0,OrangeGO.Length)];
            Instantiate(tempObject, tempPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnWait);

            xpos = -XPosition[Random.Range(0, XPosition.Length)];
            tempPos = new Vector3(xpos, 10f, 0);
            tempObject = BlueGO[Random.Range(0, BlueGO.Length)];
            Instantiate(tempObject, tempPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnWait);
        }
    }

    public void GameRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        GameOverBool = true;
        inputButton.SetActive(false);
        endPanel.SetActive(true);
        Time.timeScale = 0f;
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
        highScoreEndText.text = "HighScore : " + highScore.ToString();
    }
}
