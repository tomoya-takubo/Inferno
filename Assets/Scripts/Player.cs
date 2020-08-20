using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 移動速度最大値
    public float maxPlayerVelocity;

    // UIManager
    public UIManager uiManager;

    void Update()
    {
        // 横移動
        Move();
    }

    private void Move()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal") * maxPlayerVelocity, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Karaage")
        {
            // スコア加算
            uiManager.UpdateScore(collision.GetComponent<FireStone>().grammScore);
        }
    }
}
