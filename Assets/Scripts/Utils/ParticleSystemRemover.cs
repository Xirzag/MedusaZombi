using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ParticleSystem))]
public class ParticleSystemRemover : MonoBehaviour {

    void Update()
    {
        if (!GetComponent<ParticleSystem>().isPlaying)
            Destroy(gameObject);
    }

}
