using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaraageGenerator : MonoBehaviour
{
    public GManager gManager;                   // ゲームマネージャー

    public float addForce;                      // 打ち上げ力
    public float angularVelocity;               // 回転力
    // public float generateInterval;           // 打ち上げ間隔
    public int generateProbability;             // 生成確率分母
    public int minGenerateProbability;          // 最大生成確率分母
    public float probabilityUpdateTimeInterval;   // 確率分母更新間隔
    public int probabilityUpdateAtOnce;         // 一回当たりの確率分母更新大きさ
    public float scaleKaraageMax;               // からあげの大きさ（最大値）
    public float scaleKaraageMin;               // からあげの大きさ（最小値）
    public bool isLeft;                         // 打ち上げ場所（左側:0, 右側:1）

    public KaraageBehaviour karaagePrefab;      // からあげプレハブ格納用変数

    public Sprite[] allKaraageTypes;            // Resourcesフォルダから取得したからあげ絵を格納するための配列

    private float timeCounter = 0.0f;


    void Start()
    {
        // Resourcesフォルダのからあげ絵を取得
        allKaraageTypes = Resources.LoadAll<Sprite>("Karaage");

    }

    void Update()
    {
        GenerateKaraage();
        UpdateProbability();
    }

    private void GenerateKaraage()
    {
        if (GManager.instance.gameStates != GManager.GAMESTATES.PLAYING) return;

        if (Random.Range(0, generateProbability + 1) != 0) return;

        // からあげプレハブ作成
        KaraageBehaviour karaage = Instantiate(karaagePrefab, this.transform);

        // からあげの種類を決定
        karaage.karaageType.sprite = allKaraageTypes[Random.Range(1, 4)];

        // Z軸方向に傾ける
        float z = isLeft ? Random.Range(-5f, -25f) : Random.Range(5f, 25f);
        karaage.transform.rotation = Quaternion.Euler(karaage.transform.rotation.x
                                                    , karaage.transform.rotation.y
                                                    , z);

        // 打ち上げ力決定
        Rigidbody2D rb = karaage.GetComponent<Rigidbody2D>(); // RigidBody2D取得
        rb.AddForce(karaage.transform.up * addForce);

        // 回転力決定
        rb.angularVelocity = Random.Range(-angularVelocity, angularVelocity);

        // 大きさ決定
        float randNum = Random.Range(scaleKaraageMin, scaleKaraageMax);
        karaage.transform.localScale = new Vector2(randNum, randNum);

        // 重さ計算
        karaage.karaageWeight = randNum * 100.0f * 0.3f;
    }

    private void UpdateProbability()
    {
        timeCounter += Time.deltaTime;

        if(timeCounter > probabilityUpdateTimeInterval && generateProbability > minGenerateProbability)
        {
            generateProbability -= probabilityUpdateAtOnce;
            timeCounter = 0;
        }
    }
}
