using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class SnakeMove : MonoBehaviour
{
    private float distance = 1;
    public float time = 0.1f;
    private Vector2 dir = Vector2.right;
    private Score score;
    
    private bool gameOver = false;
    public bool GameOver => gameOver;

    public Transform topBorder;
    public Transform bottomBorder;
    public Transform leftBorder;
    public Transform rightBorder;

    public GameObject foodPrefab;
    public Transform boxSpawn;
    public Transform segmentPrefab;

    public TextMeshProUGUI gameOverText;

    private bool movementDone;

    [SerializeField] List<Transform> segments;
    void Start()
    {
        score = FindObjectOfType<Score>();
        SpawnFood();
        transform.localRotation = Quaternion.Euler(0,0,90);
        //StartCoroutine(MovementScattante());

        segments = new List<Transform>()
        { this.transform };
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W) && dir != Vector2.down)
        {
            dir = Vector2.up;
            transform.localRotation = Quaternion.Euler(0, 0, -180);
        }
        if (Input.GetKeyUp(KeyCode.S) && dir != Vector2.up)
        {
            dir = Vector2.down;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.A) && dir != Vector2.right)
        {
            dir = Vector2.left;
            transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        if (Input.GetKeyUp(KeyCode.D) && dir != Vector2.left)
        {
            dir = Vector2.right;
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    void FixedUpdate()
    {
        if (!gameOver && distance > 0)
        {
            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].position = segments[i - 1].position;
                segments[i].localRotation = segments[i - 1].localRotation;
            }
            this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + dir.x,
                Mathf.Round(this.transform.position.y) + dir.y,
                0.0f
            );
        }
    }

    //IEnumerator MovementScattante()
    //{
    //    while (distance > 0)
    //    {
    //        for (int i = segments.Count - 1; i > 0; i--)
    //        {
    //            segments[i].position = segments[i - 1].position;
    //            segments[i].localRotation = segments[i - 1].localRotation;
    //        }
    //        transform.position += dir * distance;
    //        movementDone = true;
    //        yield return new WaitForSeconds(time);
    //        movementDone = false;
    //    }
    //}

    internal void SpawnFood()
    {
        int y = (int)Random.Range(topBorder.position.y, bottomBorder.position.y);
        int x = (int)Random.Range(leftBorder.position.x, rightBorder.position.x);

        Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.name.StartsWith("Food"))
        {
            SpawnFood();
            score.points += 50;
            Destroy(c.gameObject);
            Grow();
        }
        else if (c.name.StartsWith("Wall"))
        {
            gameOverText.text = "Game Over: Wall";
            distance = 0;
            gameOver = true;
        }
        else if (c.name.StartsWith("Segment"))
        {
            gameOverText.text = ("Game Over: Segment");
            distance = 0;
            gameOver = true;
        }
    }

    internal void Grow()
    {
       Transform segment = Instantiate(this.segmentPrefab);

       segment.position = segments[segments.Count - 1].position;
       segment.localRotation = segments[segments.Count - 1].localRotation;

       segments.Add(segment);
    }
}
