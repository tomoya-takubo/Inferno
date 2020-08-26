using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxPlayerVelocity;     // プレイヤーの動く速度（最大値）

    public UIManager uiManager;         // UIマネージャー
    public List<KaraageBehaviour> getKaraageList = new List<KaraageBehaviour>();    // ゲットしたからあげ格納用リスト

    public AudioClip karaageGet;    // からあげゲット音

    void Update()
    {
        // 移動
        Move();
    }

    private void Move()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal") * maxPlayerVelocity, 0);

        float x = Mathf.Clamp(transform.position.x, -8.0f, 8.0f);
        transform.position = new Vector2(x, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Karaage")
        {
            this.GetComponent<AudioSource>().PlayOneShot(karaageGet);

            KaraageBehaviour fireSt = collision.GetComponent<KaraageBehaviour>();
            uiManager.UpdateScore(fireSt.karaageWeight);
            getKaraageList.Add(fireSt);
        }

    }
}
