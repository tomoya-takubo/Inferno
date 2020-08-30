using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explosion : MonoBehaviour
{
	private float mLength;
	private float mCur;

	// Use this for initialization
	void Start()
	{
		// アニメーション時間取得
		Animator animOne = GetComponent<Animator>();
		AnimatorStateInfo infAnim = animOne.GetCurrentAnimatorStateInfo(0);
		mLength = infAnim.length;
		mCur = 0;
	}

	// Update is called once per frame
	void Update()
	{
		// アニメーション１周期を終えたら削除
		mCur += Time.deltaTime;
		if (mCur > mLength)
		{
			GameObject.Destroy(this.transform.parent.gameObject);
		}
	}
}
