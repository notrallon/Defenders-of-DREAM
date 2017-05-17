using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_music : MonoBehaviour {
    public float MaxMusicVolume { get; private set; }

    void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
        MaxMusicVolume = GetComponent<AudioSource>().volume;
    }

    public void SwapMusic(AudioClip newSong, float fadeTime = 5.0f) {
        //StartCoroutine(MusicSwap(newSong, fadeTime));
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = newSong;
        GetComponent<AudioSource>().Play();
    }

    public void FadeInNewSong(AudioClip newSong, float fadeTime = 5.0f) {
        StopAllCoroutines();
        StartCoroutine(NewSongFadeIn(newSong, fadeTime));
    }

    private IEnumerator NewSongFadeIn(AudioClip newSong, float fadeTime) {
        float currentFadeTime = 0.0f;

        AudioSource audioSource = GetComponent<AudioSource>();

        var startTime = audioSource.time;

        audioSource.Stop();
        //audioSource.clip = newSong;

        //audioSource.PlayOneShot(newSong);
        audioSource.clip = newSong;


        Debug.Log(startTime);

//        StopCoroutine(LoopSong(startTime, newSong));
//        StartCoroutine(LoopSong(startTime, newSong));

        audioSource.Play();
        //audioSource.time = startTime;

        while (currentFadeTime < fadeTime) {
            currentFadeTime += Time.unscaledDeltaTime;

            float t = currentFadeTime / fadeTime;

            audioSource.volume = Mathf.Lerp(MaxMusicVolume, MaxMusicVolume, t);

            yield return null;
        }
    }

    private IEnumerator MusicSwap(AudioClip newSong, float fadeTime) {
        float currentFadeTime = 0.0f;

        AudioSource audioSource = GetComponent<AudioSource>();

        float startTime = audioSource.time;

        audioSource.Stop();
        //audioSource.clip = newSong;

        //audioSource.PlayOneShot(newSong);
        audioSource.clip = newSong;


        Debug.Log(startTime);

//        StopCoroutine(LoopSong(0, newSong));
//        StartCoroutine(LoopSong(0, newSong));

        audioSource.Play();
        audioSource.time = startTime;

        while (currentFadeTime < fadeTime) {
            currentFadeTime += Time.unscaledDeltaTime;

            float t = currentFadeTime / fadeTime;

            audioSource.volume = Mathf.Lerp(0, MaxMusicVolume, t);

            yield return null;
        }
    }

    private void Loop(AudioClip clip) {
        AudioSource audioSource = GetComponent<AudioSource>();

        var startTime = audioSource.time;

        audioSource.time = startTime;

        StopCoroutine(LoopSong(startTime, clip));
        StartCoroutine(LoopSong(startTime, clip));
    }

    private IEnumerator LoopSong(float startTime, AudioClip clip) {
        var aSource = GetComponent<AudioSource>();
        yield return new WaitForSecondsRealtime(Mathf.Round(aSource.clip.length - startTime));

        aSource.PlayOneShot(clip);
        Loop(clip);
    }
}
