using UnityEngine;
using System.Collections;

public class ExplosionEffect : MonoBehaviour {

    public float damage = 5f;    
    private bool playerAlreadyDamaged = false;

    void Awake()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !playerAlreadyDamaged)
        {
            other.SendMessage("DamageTaken", damage);
            playerAlreadyDamaged = true;
        }
    }
}
