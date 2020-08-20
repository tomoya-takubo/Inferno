using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    // スコアテキスト
    public Text scoreText;

    // 得点管理用変数
    public float totalScore = 0.0f;

    // スコア更新
    public void UpdateScore(float score)
    {
        // 更新
        DOTween.To(() => totalScore, (num) => totalScore = num, totalScore + score, 1.0f)
            .OnUpdate(() => scoreText.text = totalScore.ToString("F1") + " g");
    }
}
