using System;
using UnityEngine;
using TMPro;

public class Controller : MonoBehaviour
{
    public GameObject redTile;
    public GameObject greenTile;
    public GameObject blueTile;
    public float speed = 5f;
    public TMP_Text message;
    public TMP_Text gameScore;
    public TMP_Text gameTimer;
    public float time = 10f;

    private Rigidbody rb;
    private int score = 0;
    private bool stopTime = false;
    private GameObject[] tiles;
    private bool[] stop;
    private bool[] scored;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        UpdateGameTimer();
        UpdateGameScore();

        tiles = new GameObject[] { redTile, greenTile, blueTile };
        stop = new bool[tiles.Length];
        scored = new bool[tiles.Length];
    }

    void Update()
    {
        HandleMovement();
        HandleCollisionAndScore();
        UpdateGameTimer();

        if (IsGameWon())
        {
            EndGame("Win");
        }
        else if (IsGameOver())
        {
            EndGame("GameOver");
        }
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void HandleCollisionAndScore()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            CollisionDetector detector = tiles[i].GetComponent<CollisionDetector>();
            stop[i] = detector.getCompleted();
            if (!scored[i] && stop[i])
            {
                score += 5;
                UpdateGameScore();
                scored[i] = true;
            }
        }
    }

    private void UpdateGameTimer()
    {
        if (time > 0 && !stopTime)
        {
            time -= Time.deltaTime;
            string timeStr;
            if (time > 60) {
                timeStr = string.Format("{0}:{1}", Mathf.FloorToInt(time / 60), Mathf.FloorToInt(time % 60));
            }
            else {
                timeStr = string.Format("0:{0}", Mathf.FloorToInt(time));
            }
            gameTimer.text = timeStr;
        }
    }

    private void UpdateGameScore()
    {
        gameScore.text = string.Format("Score: {0}", score);
    }

    private bool IsGameWon()
    {
        return stop[0] && stop[1] && stop[2];
    }

    private bool IsGameOver()
    {
        return time <= 0;
    }

    private void EndGame(string result)
    {
        message.text = result;
        stopTime = true;
    }
}
