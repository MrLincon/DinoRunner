using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawbableObject{
        public GameObject prefab;
    }

    public SpawbableObject[] objects;

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    public float elapsedTime = 0f;
    public bool isFirstPhase = true;
    private int maxRecentSpawns = 3;  // Number of recent spawns to track
    private Queue<int> recentSpawns = new Queue<int>();

    private void OnEnable(){
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable(){
        CancelInvoke();
    }

    private void Update(){
        elapsedTime += Time.deltaTime;

        if (isFirstPhase && elapsedTime > 15f)
        {
            isFirstPhase = false;
        }
    }

    private void Spawn(){
        List<int> currentSpawnIndices = new List<int>();

        if (isFirstPhase)
        {
            // Only use the first 5 elements in the first 15 seconds
            for (int i = 0; i < 5; i++)
            {
                currentSpawnIndices.Add(i);
            }
        }
        else
        {
            // Use the last 5 elements after 15 seconds
            for (int i = 5; i < 10; i++)
            {
                currentSpawnIndices.Add(i);
            }

            // Gradually add harder elements by introducing them slowly over time
            int additionalHardElements = Mathf.FloorToInt((elapsedTime - 15f) / 5f);
            additionalHardElements = Mathf.Clamp(additionalHardElements, 0, 5);

            for (int i = 0; i < additionalHardElements; i++)
            {
                currentSpawnIndices.Add(5 + i);
            }

            // Randomly add 1 or 2 elements from the first 5 every 5-10 seconds
            if (Random.Range(0, 2) == 0) 
            {
                int count = Random.Range(1, 3); // Add 1 or 2 elements
                for (int i = 0; i < count; i++)
                {
                    int randomIndex = Random.Range(0, 5);
                    currentSpawnIndices.Add(randomIndex);
                }
            }
        }

        // Remove recent spawns from the pool to prevent repetition
        foreach (int recentIndex in recentSpawns)
        {
            currentSpawnIndices.Remove(recentIndex);
        }

        // Randomly pick an object from the current spawn indices
        int spawnIndex = currentSpawnIndices[Random.Range(0, currentSpawnIndices.Count)];
        GameObject obstacle = Instantiate(objects[spawnIndex].prefab);
        obstacle.transform.position += transform.position;

        // Track the recent spawns
        recentSpawns.Enqueue(spawnIndex);
        if (recentSpawns.Count > maxRecentSpawns)
        {
            recentSpawns.Dequeue();
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
