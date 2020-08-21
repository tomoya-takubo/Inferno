using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 移動速度最大値
    public float maxPlayerVelocity;

    // UIManager
    public UIManager uiManager;

    // 獲得したからあげリスト
    public List<FireStone> getKaraageList = new List<FireStone>();

    void Update()
    {
        // 横移動
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
            FireStone fireSt = collision.GetComponent<FireStone>();
            
            // スコア加算
            uiManager.UpdateScore(fireSt.grammScore);

            getKaraageList.Add(fireSt);
        }

    }
}
