using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class CustomAudio
{
    public AudioClip audioClip = null;
    public string name = "";
}

public class SoundManager : MonoBehaviour
{
    // for Singltone pattern
    public static SoundManager Instance { get; private set; }

    // for playing SFX, each 20 AudioSource can sound at the same time.
    private const int AUDIOMAXNUM = 20;

    public List<CustomAudio> backgroundSoundClips;
    public List<CustomAudio> effectSoundClips;

    public AudioSource bgm1;
    public AudioSource bgm2;
    public AudioSource[] audioSourceArray;
    private int sourceIdx = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        bgm1 = gameObject.AddComponent<AudioSource>();
        bgm2 = gameObject.AddComponent<AudioSource>();
        bgm1.volume = 0.0f;
        bgm2.volume = 0.0f;

        // loading all sound assets from Resources folder.
        AudioClip[] sounds = Resources.LoadAll<AudioClip>("Sounds/BGM");

        foreach (AudioClip sound in sounds)
        {
            CustomAudio tmp = new CustomAudio();
            tmp.audioClip = sound;
            tmp.name = sound.name;
            backgroundSoundClips.Add(tmp);
        }

        sounds = Resources.LoadAll<AudioClip>("Sounds/FX");
        
        foreach(AudioClip sound in sounds)
        {
            CustomAudio tmp = new CustomAudio();
            tmp.audioClip = sound;
            tmp.name = sound.name;
            effectSoundClips.Add(tmp);
        }

        audioSourceArray = new AudioSource[AUDIOMAXNUM];
        
        for (int i = 0; i < audioSourceArray.Length; i++)
        {
            audioSourceArray[i] = gameObject.AddComponent<AudioSource>();
        }

    }

    public void PlayFX(string audioName, float volume = 1.0f)
    {
        // if this AudioSource is plaing, just use other one.
        while (audioSourceArray[sourceIdx].isPlaying)
        {
            sourceIdx++;
            if(sourceIdx >= AUDIOMAXNUM)
            {
                sourceIdx = 0;
            }
        }
        CustomAudio sound = effectSoundClips.Find(source => source.name == audioName);
        if (sound == null)
        {
            Debug.Log("Sound " + audioName + " is missing");
            return;
        }
        audioSourceArray[sourceIdx].clip = sound.audioClip;
        audioSourceArray[sourceIdx].loop = false;
        audioSourceArray[sourceIdx].volume = volume;
        audioSourceArray[sourceIdx].Play();
    }

    public void PlayBgm(string audioName, float volume = 1.0f, float fadeDuration = 4.0f)
    {
        if (fadeDuration == 0.0f)
        {
            return;
        }
        StopAllCoroutines();
        //CustomAudio sound = Array.Find(backgroundSoundClips, source => source.name == audioName);
        CustomAudio sound = backgroundSoundClips.Find(source => source.name == audioName);

        if (bgm1.isPlaying)
        {
            bgm2.clip = sound.audioClip;
            StartCoroutine(PlaySoundFadeIn(bgm2, fadeDuration, volume));
            StartCoroutine(StopSoundFadeIn(bgm1, fadeDuration));

        }
        else if(bgm2.isPlaying)
        {
            bgm1.clip = sound.audioClip;
            StartCoroutine(PlaySoundFadeIn(bgm1, fadeDuration, volume));
            StartCoroutine(StopSoundFadeIn(bgm2, fadeDuration));
        }
        else
        {
            bgm1.clip = sound.audioClip;
            StartCoroutine(PlaySoundFadeIn(bgm1, fadeDuration, volume));
        }
    }

    private IEnumerator PlaySoundFadeIn(AudioSource source, float duration, float volume)
    {
        
        source.loop = true;
        source.Play();

        float tmpVolume = source.volume;

        float factor = (volume-tmpVolume) / (duration * 50);

        
        while(true)
        {
            yield return new WaitForSeconds(0.01f);
            tmpVolume += factor;
            source.volume = tmpVolume;
            if (source.volume >= volume)
            {
                source.volume = volume;
                break;
            }
        }
    }

    private IEnumerator StopSoundFadeIn(AudioSource source, float duration)
    {

        float tmpVolume = source.volume;
        float factor = tmpVolume / (duration * 50);

        
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            tmpVolume -= factor;
            source.volume = tmpVolume;
            if (source.volume <= 0.0f)
            {
                source.Stop();
                break;
            }
        }
    }

}
