using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

[System.Serializable]
public struct SpawbableObject{
    public GameObject prefab;
    [Range(0f, 1f)]
    public float weight;
}

public SpawbableObject[] objects;

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private void OnEnable(){
       Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable(){
        CancelInvoke();
    }

    private void Spawn(){
        float spawnChance = Random.value;

        foreach (var obj in objects)
        {
            if(spawnChance < obj.weight){
               GameObject obstacle = Instantiate(obj.prefab);
               obstacle.transform.position += transform.position;
               break;
            }else{
                spawnChance -= obj.weight;
            }
        }
         Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

 
}
