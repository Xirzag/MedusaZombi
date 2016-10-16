using UnityEngine;
using System.Collections;

public class PileOfBullets : MonoBehaviour {

    public int amountOfBullets = 15;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponentInChildren<Gun>().Bullets += amountOfBullets;
            Destroy(gameObject);
        }
    }

}
