using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GManager gManager;           // ゲームマネージャー
    public Result result;               // リザルト  
    public Text touchScreen;            // タッチトゥスクリーン
    public GameObject retry;            // リトライ
    public GameObject player;
    public GameObject destroyZone;      // ステージ外
    public GameObject title;            // タイトル

    public Text scoreText;              // 得点表示
    public Text timeLimitText;          // 時間表示
    public float totalScore = 0.0f;     // 得点
    public float timeLimit = 60.0f;     // 制限時間

    private float timeCountMax = 0.5f;  // TouchScreen点滅間隔   
    private float timeCounter;          // TouchScreen点滅経過時間

    void Start()
    {
        scoreText.text = totalScore.ToString("F1") + " g";
    }

    void Update()
    {
        WaitForStart();

        // CountTime();
    }

    /// <summary>
    /// スコア更新
    /// </summary>
    /// <param name="score"></param>
    public void UpdateScore(float score)
    {
        // DOTween.To(() => totalScore, (num) => totalScore = num, totalScore + score, 1.0f)
        //     .OnUpdate(() => scoreText.text = totalScore.ToString("F1") + " g");     // カウントアップするように更新

        totalScore += score;
    }

    /// <summary>
    /// 残り時間更新
    /// </summary>
    private void CountTime()
    {
        if (GManager.instance.gameStates == GManager.GAMESTATES.GAMEOVER) return;  // ゲームオーバーなら後続処理省略

        timeLimit -= Time.deltaTime;    // 残り時間計算

        if(timeLimit <= 0.0f)
        {
            timeLimit = 0.0f;           // 0以下の場合は0
            Instantiate(result);

            GameObject[] karaages = GameObject.FindGameObjectsWithTag("Karaage");
            foreach(GameObject karaage in karaages)
            {
                if (karaage.activeSelf) Destroy(karaage);
            }

            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x
                                                       , 153
                                                       , Camera.main.transform.position.z);
            Camera.main.GetComponent<Camera>().orthographicSize = 10;
        }

        timeLimitText.text = timeLimit.ToString("F0") + " s";    // 表示更新
    }

    /// <summary>
    /// 画面タッチ処理
    /// </summary>
    public void WaitForStart()
    {
        if (GManager.instance.gameStates != GManager.GAMESTATES.OPENING) return;

        timeCounter += Time.deltaTime;
        if(timeCounter > timeCountMax)
        {
            touchScreen.enabled = !touchScreen.enabled;
            timeCounter = 0.0f;
        }
    }

    public void TouchScreen()
    {
        title.SetActive(false);
        GManager.instance.gameStates = GManager.GAMESTATES.PLAYING;
    }

    public void ShowRetryButton()
    {
        retry.SetActive(true);
    }

    public void Retry()
    {
        retry.SetActive(false);
        destroyZone.SetActive(true);
        GManager.instance.gameStates = GManager.GAMESTATES.OPENING;
        SceneManager.LoadScene("SampleScene");
    }
}
