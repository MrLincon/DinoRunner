using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance { get; private set; }

   public float initialGameSpeed = 5f;
   public float gameSpeedIncrease = 0.1f;
   public float gameSpeed { get; private set; }

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
        NewGame();
    }

    public void NewGame()
    {
        gameSpeed = initialGameSpeed;
    }

    public void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
    }
}
