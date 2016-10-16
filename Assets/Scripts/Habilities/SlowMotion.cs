using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System;

public class SlowMotion : MonoBehaviour {

    private bool casting = false;
    public float maxSlow = 0.4f;
    public float slowTime = 2f;
    public float transitionTime = 0.3f;
    public AnimationCurve slowCurve;
    public AudioMixer masterMixer;

    private enum transition{ begin = 0, end = 1};

    IEnumerator castCoroutine()
    {

        masterMixer.SetFloat("Pitch",1.0f);

        yield return StartCoroutine(Transition(transition.begin));

        masterMixer.SetFloat("Pitch", 0.5f);

        yield return new WaitForSecondsRealtime( slowTime );

        yield return StartCoroutine(Transition(transition.end));

        masterMixer.SetFloat("Pitch", 1.0f);
    }

    public void cast()
    {
        if (!casting)
            StartCoroutine(castCoroutine());
    }

    private IEnumerator Transition(transition mode)
    {
        
        float start = Time.realtimeSinceStartup;
        float progress = (int)mode;

        while ((mode == transition.begin) ? (progress < 1) : (progress > 0))
        {
            float timeScale = Mathf.Lerp(maxSlow, 1, slowCurve.Evaluate(progress));
            if(mode == transition.begin)
                progress = (Time.realtimeSinceStartup - start) / transitionTime;
            else
                progress = 1 - (Time.realtimeSinceStartup - start) / transitionTime;
            Time.timeScale = timeScale;
            masterMixer.SetFloat("Pitch", Mathf.Lerp(1, 0.5f, progress));
            yield return 0;
        }
    }
}
