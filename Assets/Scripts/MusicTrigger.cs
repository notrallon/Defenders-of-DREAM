using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour {

	private GameObject m_AudioControllerObject;
	private AudioSource m_AudioSource;
	private bg_music m_MusicController;
	private AudioClip m_DefaultClip;
	private AudioClip m_ThisClip;

	private float m_FadeInTime = 5.0f;

	// Use this for initialization
	void Start () {
		m_AudioControllerObject = GameObject.FindGameObjectWithTag("Music");
		m_AudioSource = m_AudioControllerObject.GetComponent<AudioSource>();
		m_MusicController = m_AudioControllerObject.GetComponent<bg_music>();
		m_DefaultClip = m_AudioSource.clip;
		m_ThisClip = GetComponent<AudioSource>().clip;
		GetComponent<AudioSource>().volume = 0;
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("MainCamera")) {
			if (GetComponent<AudioSource>().volume < m_MusicController.MaxMusicVolume) {
				//m_MusicController.FadeInNewSong(GetComponent<AudioSource>().clip, 0.5f);
				StopCoroutine(FadeInSong(m_DefaultClip, GetComponent<AudioSource>(), m_AudioSource));
				StartCoroutine(FadeInSong(GetComponent<AudioSource>().clip, m_AudioSource, GetComponent<AudioSource>()));
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.CompareTag("MainCamera")) {
			if (m_AudioSource.volume < m_MusicController.MaxMusicVolume) {
				StopCoroutine(FadeInSong(GetComponent<AudioSource>().clip, m_AudioSource, GetComponent<AudioSource>()));
				StartCoroutine(FadeInSong(m_DefaultClip, GetComponent<AudioSource>(), m_AudioSource));
			}
			//m_MusicController.FadeInNewSong(m_DefaultClip, 0.5f);

		}
	}

	private IEnumerator FadeInSong(AudioClip newClip, AudioSource from, AudioSource too) {
		var currentFadeInTime = 0.0f;

		var fromTime = m_AudioSource.time;
		too.Play();

		too.time = fromTime;

		var oldFromVolume = from.volume;
        var oldTooVolume = too.volume;

		too.loop = true;
		from.loop = true;

		while (currentFadeInTime < m_FadeInTime) {
			currentFadeInTime += Time.deltaTime;

			var t = currentFadeInTime / m_FadeInTime;

			from.volume = Mathf.Lerp(oldFromVolume, 0, t);
			too.volume = Mathf.Lerp(oldTooVolume, m_MusicController.MaxMusicVolume, t);

			yield return null;
		}

		from.Stop();
	}
}
