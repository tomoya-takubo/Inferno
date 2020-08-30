using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKaraageAria : MonoBehaviour
{
    public AudioClip karaageGet;
    public UIManager uiManager;
    public List<KaraageBehaviour> getKaraageList = new List<KaraageBehaviour>();
    private KaraageBehaviour karaage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GManager.instance.gameStates != GManager.GAMESTATES.PLAYING) return;

        // からあげ
        if (collision.tag == "Karaage")
        {
            this.GetComponent<AudioSource>().PlayOneShot(karaageGet);

            karaage = collision.GetComponent<KaraageBehaviour>();
            uiManager.UpdateScore(karaage.karaageWeight);
            getKaraageList.Add(karaage);
        }
    }
}
