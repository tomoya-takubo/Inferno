using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Result : MonoBehaviour
{
    public GetKaraageAria getKaraageAria;
    public float resultInterval;    // リザルト画面でからあげを生成する時間

    void Start()
    {
        getKaraageAria = GameObject.FindWithTag("GetKaraageAria").GetComponent<GetKaraageAria>();
        CountScoreText();
        StartCoroutine(ShowResult());
    }

    private void CountScoreText()
    {
        getKaraageAria.uiManager.scoreText.gameObject.SetActive(true);

        float iniValue = 0.0f;
        DOTween.To
        (
            () => iniValue
            , (num) => { iniValue = num; getKaraageAria.uiManager.scoreText.text = num.ToString("F1") + " g GET!"; }
            , getKaraageAria.uiManager.totalScore, getKaraageAria.getKaraageList.Count * resultInterval
        );     // カウントアップするように更新
    }

    IEnumerator ShowResult()
    {
        List<KaraageBehaviour> tmpKaraageBehaviour = getKaraageAria.getKaraageList;

        foreach (KaraageBehaviour karaage in tmpKaraageBehaviour)
        {
            if (karaage == null) continue;

            float x = this.transform.position.x + Random.Range(-3.0f, 3.0f);
            karaage.transform.position = new Vector2(x, this.transform.position.y);
            karaage.gameObject.SetActive(true);
            karaage.transform.Find("Weight").gameObject.SetActive(false);          // グラム数表示を非表示

            yield return new WaitForSeconds(resultInterval);
        }

        GManager.instance.gameStates = GManager.GAMESTATES.RESULTED;
        getKaraageAria.uiManager.ShowRetryButton();
    }
}
