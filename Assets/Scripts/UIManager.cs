using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public GManager gManager;           // ゲームマネージャー
    public Result result;               // リザルト      

    public Text scoreText;              // 得点表示
    public Text timeLimitText;          // 時間表示
    public float totalScore = 0.0f;     // 得点
    public float timeLimit = 60.0f;     // 制限時間

    void Update()
    {
        CountTime();
    }

    /// <summary>
    /// スコア更新
    /// </summary>
    /// <param name="score"></param>
    public void UpdateScore(float score)
    {
        DOTween.To(() => totalScore, (num) => totalScore = num, totalScore + score, 1.0f)
            .OnUpdate(() => scoreText.text = totalScore.ToString("F1") + " g");     // カウントアップするように更新
    }

    /// <summary>
    /// 残り時間更新
    /// </summary>
    private void CountTime()
    {
        if (gManager.gameOver) return;  // ゲームオーバーなら後続処理省略

        timeLimit -= Time.deltaTime;    // 残り時間計算

        if(timeLimit <= 0.0f)
        {
            timeLimit = 0.0f;           // 0以下の場合は0
            gManager.gameOver = true;   // ゲームオーバー
            Instantiate(result);

            GameObject[] karaages = GameObject.FindGameObjectsWithTag("Karaage");
            foreach(GameObject karaage in karaages)
            {
                if (karaage.activeSelf) Destroy(karaage);
            }

            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x
                                                       , 150
                                                       , Camera.main.transform.position.z);
            Camera.main.GetComponent<Camera>().orthographicSize = 10;
        }

        timeLimitText.text = timeLimit.ToString("F0") + " s";    // 表示更新
    }
}
