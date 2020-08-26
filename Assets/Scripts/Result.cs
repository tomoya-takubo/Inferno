using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    public Player player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        StartCoroutine(ShowResult());
    }

    IEnumerator ShowResult()
    {
        foreach (KaraageBehaviour karaage in player.getKaraageList)
        {
            float x = this.transform.position.x + Random.Range(-2.0f, 2.0f);
            karaage.transform.position = new Vector2(x, this.transform.position.y);
            karaage.gameObject.SetActive(true);
            karaage.transform.Find("Weight").gameObject.SetActive(false);          // グラム数表示を非表示

            yield return new WaitForSeconds(0.3f);
        }
    }
}
