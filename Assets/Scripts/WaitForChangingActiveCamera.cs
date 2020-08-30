using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForChangingActiveCamera : MonoBehaviour
{
    public Result resultPrefab;   // リザルト参照
    public float waitTime;  // カメラ切り替えまでの待ち時間

    void Start()
    {
        StartCoroutine("ChangeActiveCamera");
    }

    IEnumerator ChangeActiveCamera()
    {
        yield return new WaitForSeconds(waitTime);

        Camera.main.gameObject.SetActive(false);
        Instantiate(resultPrefab);
    }
}
