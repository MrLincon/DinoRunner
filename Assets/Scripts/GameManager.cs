using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }

   public float initialGameSpeed = 5f;
   public float gameSpeedIncrease = 0.1f;
   public float gameSpeed { get; private set; }

   private Player player;
   private Spawner spawner;

    public Button startGame;
   public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
   public Button retry;

   private float score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {

        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        enabled = false;
        startGame.gameObject.SetActive(true);
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);
       

    }

    public void NewGame()
    {

        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (Obstacle obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        gameSpeed = initialGameSpeed;
        enabled = true;
        
        startGame.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        startGame.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        retry.gameObject.SetActive(false);
    

        score = 0;
        scoreText.text = Mathf.FloorToInt(score).ToString("D6");


    }

    public void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += Time.deltaTime * gameSpeed;

        scoreText.text = Mathf.FloorToInt(score).ToString("D6");
    }

    public void GameOver()
    {
        gameSpeed = 0;
        enabled = false;
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);

        gameOverText.gameObject.SetActive(true);
        retry.gameObject.SetActive(true);

        spawner.elapsedTime = 0f;
        spawner.isFirstPhase = true;

        

    }
}
