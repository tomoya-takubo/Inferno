using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public float maxPlayerVelocity;                                         // プレイヤーの動く速度（最大値）
    public UIManager uiManager;                                             // UIマネージャー
    public Image hp;                                                        // HP
    public float explosionDamage = 0.3f;                                    // 爆風に巻き込まれたときのダメージ
    public WaitForChangingActiveCamera WaitForChangingActiveCameraPrefab;   // 結果
    public GameObject explosionPrefab;                                       // 爆発プレハブ                                             
    private Rigidbody2D rb;
    private KaraageBehaviour fireSt;

    public float flashingSpan = 0.2f;               // 点滅間隔（ダメージ時）
    public float invincibleTime = 1.0f;             // 無敵時間
    public bool isInvincible = false;               // 無敵かどうか
    private float timeCounter = 0.0f;               // タイムカウンター
    private float totalTime = 0.0f;                 // 総時間経過
    public AudioClip damaged;                       // 被ダメージ音
    public GameObject destroyZone;                  // ステージ外
    private BoxCollider2D boxCo;
    private SpriteRenderer sptRen;

    void Start()
    {
        this.rb = this.gameObject.GetComponent<Rigidbody2D>();
        this.boxCo = this.GetComponent<BoxCollider2D>();
        this.sptRen = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (GManager.instance.gameStates != GManager.GAMESTATES.PLAYING) return;

        // 移動
        Move();
    }

    private void Move()
    {
        this.rb.velocity = new Vector2(Input.GetAxis("Horizontal") * maxPlayerVelocity, 0);

        float x = Mathf.Clamp(transform.position.x, -8.0f, 8.0f);
        transform.position = new Vector2(x, transform.position.y);

        // 被ダメージ時
        Flashing();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GManager.instance.gameStates != GManager.GAMESTATES.PLAYING) return;

        // 爆風
        if(collision.tag == "Explosion" && !isInvincible)
        {
            hp.fillAmount -= explosionDamage;

            isInvincible = true;
            boxCo.enabled = false;
            sptRen.enabled = false;

            this.GetComponent<AudioSource>().PlayOneShot(damaged);

            if (hp.fillAmount == 0)
            {
                GManager.instance.gameStates = GManager.GAMESTATES.GAMEOVER;
                this.rb.velocity = new Vector2(0, 0);
                GManager.instance.GameOverWhistle();
                destroyZone.SetActive(false);
                Instantiate(explosionPrefab, this.transform);
                Instantiate(WaitForChangingActiveCameraPrefab);
            }
        }
    }

    /// <summary>
    /// 被ダメージ時
    /// </summary>
    /// <param name="_isInvincible"></param>
    private void Flashing()
    {
        if (!isInvincible) return;

        float dlt = Time.deltaTime;
        timeCounter += dlt;
        totalTime += dlt;

        if(timeCounter > flashingSpan)
        {
            sptRen.enabled = !sptRen.enabled;
            timeCounter = 0;
        }

        if(totalTime > invincibleTime)
        {
            timeCounter = 0;
            totalTime = 0;

            boxCo.enabled = true;
            sptRen.enabled = true;
            isInvincible = false;
        }
    }
}
