using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BriefingController : MonoBehaviour {

	[SerializeField]
	private Image m_Background;

	private const float BG_FADEOUT_TIME = 10.0f;
	private const float BRIEFING_FADEOUT_TIME = 2.0f;
	private float m_CurrentFadeoutTime = 0.0f;

	private Color m_BgStartColor;
	private Color m_BgTargetColor;

	[SerializeField]
	private Image m_BriefingImage;

	private Color m_BriefingStartColor;
	private Color m_BriefingTargetColor;

	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		m_BgStartColor = m_Background.color;
		m_BgTargetColor = m_BgStartColor;
		m_BgTargetColor.a = 0;

		m_BriefingStartColor = m_BriefingImage.color;
		m_BriefingTargetColor = m_BriefingStartColor;
		m_BriefingTargetColor.a = 0;

		var sortinglayer = gameObject.GetComponent<Canvas>().sortingOrder;
		Debug.Log(sortinglayer);

		StartCoroutine(FadeOutBackground());
		//m_Background.CrossFadeAlpha(0, BG_FADEOUT_TIME, true);
	}

	private IEnumerator FadeOutBackground() {
		while (m_CurrentFadeoutTime < BG_FADEOUT_TIME) {
			m_CurrentFadeoutTime += Time.unscaledDeltaTime;

			var t = m_CurrentFadeoutTime / BG_FADEOUT_TIME;

			t = 1f - Mathf.Cos(t * t * t  * Mathf.PI * 0.5f);

			m_Background.color = Color.Lerp(m_BgStartColor, m_BgTargetColor, t);

			yield return null;
		}
		Time.timeScale = 1;
		StartCoroutine(FadeOutBriefing());
		StopCoroutine(FadeOutBackground());
	}

	private IEnumerator FadeOutBriefing() {
		m_CurrentFadeoutTime = 0.0f;

		while (m_CurrentFadeoutTime < BRIEFING_FADEOUT_TIME) {
			m_CurrentFadeoutTime += Time.unscaledDeltaTime;

			var t = m_CurrentFadeoutTime / BRIEFING_FADEOUT_TIME;

			t = t * t;

			m_BriefingImage.color = Color.Lerp(m_BriefingStartColor, m_BriefingTargetColor, t);

			yield return null;
		}
	}
}
