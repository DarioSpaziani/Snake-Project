using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{
    public float speed;
    private bool gameOver;
    public bool GameOver => gameOver;

    public float vertical = 1;
    public float horizontal = 0;
    public float distance = 1;

    private Vector2 direction;

    public Transform topBorder;
    public Transform bottomBorder;
    public Transform leftBorder;
    public Transform rightBorder;

    public Transform segmentPrefab;

    public GameObject foodPrefab;
    private Score score;
    private TouchPhaseDisplay touchControls;

    [SerializeField]List<Transform> segments;

    private void Start()
    {
        touchControls = FindObjectOfType<TouchPhaseDisplay>();

        transform.localRotation = Quaternion.Euler(0,0,90);
        score = FindObjectOfType<Score>();
        SpawnFood();
        segments = new List<Transform>
        {
            this.transform
        };
        vertical = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down){
            UpMove();
        } else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up) { 
            DownMove();
        } else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left) { 
            RightMove();
        } else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right) { 
            LeftMove();
        }
    }

    public void UpMove()
    {
        if(direction != Vector2.down) 
        {
            transform.localRotation = Quaternion.Euler(0, 0, -180);
            vertical = 1;
            horizontal = 0;
        }
    }
    public void DownMove()
    {
        if(direction != Vector2.up)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            vertical = -1;
            horizontal = 0;
        }
    }
    public void RightMove()
    {
        if (direction != Vector2.left)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 90);
            horizontal = 1;
            vertical = 0;
        }
    }
    public void LeftMove()
    {
        if (direction != Vector2.right)
        {
            transform.localRotation = Quaternion.Euler(0, 0, -90);
            horizontal = -1;
            vertical = 0;
        }
    }

    void FixedUpdate()
    {
        if(!gameOver && speed > 0)
        {
            for(int i = segments.Count-1; i > 0; i--)
            {
                segments[i].position = segments[i - 1].position;
                segments[i].localRotation = segments[i - 1].localRotation;
            }
            this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + Mathf.Clamp(touchControls.xDir,-1,1),
                Mathf.Round(this.transform.position.y) + Mathf.Clamp(touchControls.yDir,-1,1),
                0.0f
            );
        }
    }

    internal void SpawnFood()
    {
        int y = (int)Random.Range(topBorder.position.y, bottomBorder.position.y);
        int x = (int)Random.Range(leftBorder.position.x, rightBorder.position.x);

        Instantiate(foodPrefab, new Vector2(x,y), Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (gameObject.CompareTag("Food"))
        {
            SpawnFood();
            score.points += 50;
            Destroy(c.gameObject);
            Grow();
        }
        else if (c.name.StartsWith("Wall"))
        {
            Debug.Log("Game Over");
            speed = 0;
            gameOver = true;
        }
    }

    internal void Grow()
    {
        Vector3 spawnPos = segments[segments.Count - 1].position;
        Transform segment = Instantiate(this.segmentPrefab,spawnPos,Quaternion.identity);
        segment.position = segments[segments.Count - 1].position;
        segment.localRotation = segments[segments.Count - 1].localRotation;

        segments.Add(segment);
    }
}
