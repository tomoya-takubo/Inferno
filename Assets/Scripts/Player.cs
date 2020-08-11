using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 移動速度最大値
    public float maxPlayerVelocity;

    void Update()
    {
        // 横移動
        Move();
    }

    private void Move()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal") * maxPlayerVelocity, 0);
    }
}
