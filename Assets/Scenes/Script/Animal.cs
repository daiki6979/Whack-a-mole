using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animal : MonoBehaviour
{
    public GameObject[] animalPrefabs;   // 動物のPrefab
    public int triggerScore = 10;     // 発生条件（50点）

    public Vector3 spawnPosition;     // 出現位置（Inspectorで調整）

    bool hasSpawned = false;

    public static bool IsJamming = false;   //イベント中のスコア増加を防ぐ

    GameObject spawnedAnimal;         // ← 生成した動物を保持

    public MicCapture mic;
    float clearVolume = 0.25f; //イベント終了条件の音量


    public Slider micGauge;
    public GameObject Event;
    public RectTransform thresholdUI;

    void Start()
    {
        UpdateThresholdLine();
    }

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
            micGauge.value = Mathf.Clamp01(mic.rms);

            if (mic.rms > clearVolume)
            {
                RemoveAnimal();
            }
        }

    }

    void SpawnAnimal()
    {
        if (animalPrefabs.Length == 0) return;

        int index = Random.Range(0, animalPrefabs.Length);
        GameObject animalprefab = animalPrefabs[index];

        spawnedAnimal = Instantiate(animalprefab, FindObjectOfType<Canvas>().transform);
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

    //clearVolumeに対応した箇所に赤線を表示
    void UpdateThresholdLine()
    {
        if (micGauge != null && thresholdUI != null)
        {
            RectTransform area = micGauge.fillRect.parent as RectTransform;

            float height = area.rect.height;
            float width  = area.rect.width;

            thresholdUI.sizeDelta = new Vector2(width, 4f);

            float y = Mathf.Lerp(-height * 0.5f, height * 0.5f, clearVolume);

            thresholdUI.anchoredPosition = new Vector2(0, y);
        }
    }
}
