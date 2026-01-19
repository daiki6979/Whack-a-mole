using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public GameObject animalPrefab;   // 動物のPrefab
    public int triggerScore = 50;     // 発生条件（50点）

    public Vector3 spawnPosition;     // 出現位置（Inspectorで調整）

    bool hasSpawned = false;

    GameObject spawnedAnimal;         // ← 生成した動物を保持

    void Update()
    {
        // スコア条件で出現
        if (!hasSpawned && ScoreManager.Instance.Score >= triggerScore)
        {
            SpawnAnimal();
            hasSpawned = true;
        }

        // Fキーで動物を消す
        if (Input.GetKeyDown(KeyCode.F))
        {
            RemoveAnimal();
        }
    }

    void SpawnAnimal()
    {
        spawnedAnimal = Instantiate(animalPrefab, spawnPosition, Quaternion.identity);
    }

    void RemoveAnimal()
    {
        if (spawnedAnimal != null)
        {
            Destroy(spawnedAnimal);
            spawnedAnimal = null;
            //hasSpawned = false; // ← 再出現を許可したい場合
        }
    }
}
