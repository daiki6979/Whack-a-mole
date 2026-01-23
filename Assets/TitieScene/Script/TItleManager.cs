using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TItleManager : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;

    bool istarting = false;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        if (istarting) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
        //CheckPullByZValue();
    }

    IEnumerator FadeIn()
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f;
    }

    public void StartGame()
    {
        StartCoroutine(FadeOutAndLoad("野菜引っこ抜き"));
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
            yield return null;
        }
        SceneManager.LoadScene("SampleScene");
    }

    /*void CheckPullByZValue()
    {
        if (Recelver.Instance == null) return;

        float zAcc = Recelver.Instance.acc.z;

        //初期のZの値を取得（基準値）
        if (!isBaseSet)
        {
            baseZ = zAcc;
            isBaseSet = true;
            return;
        }

        float deltaZ = zAcc - baseZ;
        Debug.Log(deltaZ);


        //閾値を超えたら
        if (canPullByAcc && deltaZ < pullThresholdZ)
        {
            StartGame();
            canPullByAcc = false;
        }

        //加速度の閾値が戻る
        if (zAcc > 1.0f)
        {
            canPullByAcc = true;
        }

    }*/
}
