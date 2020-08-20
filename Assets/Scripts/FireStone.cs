using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStone : MonoBehaviour
{
    // からあげグラム数
    public float grammScore;

    // 
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "DestroyZone" || col.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
