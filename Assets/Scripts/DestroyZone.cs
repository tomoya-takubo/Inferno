using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    public GameObject explosionPrefab;  // 爆発プレハブ

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Karaage")
        {
            GameObject explosion = Instantiate(explosionPrefab
                                            , col.transform.position
                                            , Quaternion.identity);
            explosion.transform.SetParent(this.transform);
        }
    }
}
