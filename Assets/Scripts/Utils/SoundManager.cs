using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {

    private AudioSource source;

    [System.Serializable]
    public struct State
    {
        public string name;
        public AudioClip[] sounds;
        public float timeBetweenSounds;
        public bool onlyOnce;
    }

    
    public State[] states;

    private const int StopPlaying = -1;
    private int playingId = StopPlaying;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(PlayCorutine());
    }

    public void Play(int id)
    {
        playingId = id;
    }

    public void Play(string name)
    {
        for (int i = 0; i < states.Length; i++)
            if (states[i].name == name)
                Play(i);
        
    }


    public void Stop()
    {
        playingId = StopPlaying;
    }

    private IEnumerator PlayCorutine()
    {

        while(true)
        {
            yield return new WaitWhile(() => playingId == StopPlaying);

            State actualState = states[playingId];

            int id = Random.Range(0, actualState.sounds.Length);
            source.PlayOneShot(actualState.sounds[id]);

            if (actualState.onlyOnce)
                Stop();
            else
                yield return new WaitForSeconds(actualState.timeBetweenSounds);

        }

    }
}
