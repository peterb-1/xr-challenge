using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    AudioMixer mixer;
    [SerializeField]
    AudioSource[] sounds;

    public static AudioManager instance { get; private set; }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    /// <summary>
	/// Play the sound with the given name
	/// </summary>
    public void Play(string sound)
    {
        foreach (AudioSource s in sounds)
        {
            if (s.name == sound)
            {
                s.Play();
                return;
            }
        }
    }

    /// <summary>
	/// Stop the sound with the given name
	/// </summary>
    public void Stop(string sound)
    {
        foreach (AudioSource s in sounds)
        {
            if (s.name == sound)
            {
                s.Stop();
                return;
            }
        }
    }

    /// <summary>
	/// Initiate a low-pass filter fade
	/// </summary>
    public void LowPass(float end, float time)
    {
        float start;
        mixer.GetFloat("CutoffFreq", out start);
        StartCoroutine(LowPassFade(start, end, time));
    }

    /// <summary>
	/// Carry out a low-pass filter fade
	/// </summary>
    IEnumerator LowPassFade(float start, float end, float time)
    {
        float currentTime = 0;
        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            mixer.SetFloat("CutoffFreq", Mathf.Lerp(start, end, currentTime / time));
            yield return null;
        }
        mixer.SetFloat("CutoffFreq", end);
    }
}
