using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;//他のスクリプトからPopupManager.Instanceでアクセス
    public Text scoreText;

    int  score = 0;//開始時のスコア

    public int Score
    {
        get { return score; }
    }

    //変更点
    void Awake()
    {
        // 二重生成防止
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        scoreText.text = "Score : 0";
    }

    //ここで表示をまとめて更新
    void LateUpdate()
    {
        scoreText.text = "Score : " + score;
    }

    // スコア加算（表示更新はしない）
    public void AddScore(int value)
    {
        score += value;
    }

}
