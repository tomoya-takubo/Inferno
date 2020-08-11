using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStoneGenerator : MonoBehaviour
{
    // 火山岩プレハブ
    public FireStone fireStonePrefab;

    // 上向きに加える力
    public float addForce;

    // 角速度
    public float angularVelocity;

    // 生成する時間間隔
    public float generateInterval;

    IEnumerator Start()
    {
        // 火山岩生成
        while(true)
        {
            // 火山岩生成
            FireStone fireStone = Instantiate(fireStonePrefab, this.transform);

            // Z軸方向に回転
            fireStone.transform.rotation = Quaternion.Euler(fireStone.transform.rotation.x
                                                            , fireStone.transform.rotation.y
                                                            , Random.Range(-5f, -25f));

            // 速度を与える
            fireStone.GetComponent<Rigidbody2D>().AddForce(fireStone.transform.up * addForce);

            // 角速度を与える
            fireStone.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-angularVelocity, angularVelocity);

            // ***秒待つ
            yield return new WaitForSeconds(generateInterval);

        }
    }
}
