using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaraageBehaviour : MonoBehaviour
{
    public TextMesh weight;                 // グラム数表示

    public float karaageWeight;             // からあげの重さ（グラム）
    public SpriteRenderer karaageType;      // からあげの種類

    public AudioClip falling;               // 落下音

    public bool isFalling = false;          // 落下しているか

    void Start()
    {
        weight.text = karaageWeight.ToString("F1") + " g";

        weight.GetComponent<MeshRenderer>().sortingLayerName = "Default";
        weight.GetComponent<MeshRenderer>().sortingOrder = 1;
    }

    void Update()
    {
        if (isFalling) return;

        if(this.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            this.GetComponent<AudioSource>().PlayOneShot(falling);
            isFalling = true;
        }
    }

    /// <summary>
    /// 接触時処理
    /// </summary>
    /// <param name="col"></param>
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }


        if (col.tag == "DestroyZone")
        {
            Destroy(this.gameObject);
        }
    }
}
