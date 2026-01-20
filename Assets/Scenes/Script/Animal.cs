using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animal : MonoBehaviour
{
    public GameObject animalPrefab;   // 動物のPrefab
    public int triggerScore = 10;     // 発生条件（50点）

    public Vector3 spawnPosition;     // 出現位置（Inspectorで調整）

    bool hasSpawned = false;

    public static bool IsJamming = false;   //イベント中のスコア増加を防ぐ

    GameObject spawnedAnimal;         // ← 生成した動物を保持

    public MicCapture mic;
    public float clearVolume = 0.4f;

    public Slider micGauge;
    public GameObject Event;

    void Update()
    {
        // スコア条件で出現
        if (!hasSpawned && ScoreManager.Instance.Score >= triggerScore)
        {
            SpawnAnimal();
            hasSpawned = true;
            triggerScore += 50;
        }

        // Fキーで動物を消す
        if (Input.GetKeyDown(KeyCode.F))
        {
            RemoveAnimal();
        }

        // 音で動物を消す
        if (Animal.IsJamming && mic != null)
        {
            micGauge.value = Mathf.Clamp01(mic.rms * 10f);

            if (mic.rms > clearVolume)
            {
                RemoveAnimal();
            }
        }

    }

    void SpawnAnimal()
    {
        spawnedAnimal = Instantiate(animalPrefab, FindObjectOfType<Canvas>().transform);
        spawnedAnimal.transform.localPosition = spawnPosition;
        IsJamming = true;
        if(Event != null)
        {
            Event.SetActive(true);
        }
    }


    void RemoveAnimal()
    {
        if (spawnedAnimal != null)
        {
            Destroy(spawnedAnimal);
            spawnedAnimal = null;
            IsJamming = false;
            if(Event != null)
            {
                Event.SetActive(false);
            }
            hasSpawned = false; // ← 再出現を許可したい場合
        }
    }
}
