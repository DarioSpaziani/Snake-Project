using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public GameObject gameOver;
    public GameObject restart;
    public int points;
    private SnakeMove snake;

    private void Awake()
    {
        snake = FindObjectOfType<SnakeMove>();
    }
    private void Start()
    {
        StartCoroutine(PointsAdder());
    }
    private void Update()
    {
        ScoreUpdate();
    }

    void ScoreUpdate()
    {
        pointsText.text = "Score : " + points;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator PointsAdder()
    {
        while (!snake.GameOver)
        {
            points++;
            yield return new WaitForSeconds(1f);
        }
        gameOver.SetActive(true);
        restart.SetActive(true);
    }
}
