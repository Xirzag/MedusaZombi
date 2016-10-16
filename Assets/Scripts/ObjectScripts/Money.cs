using UnityEngine;
using System.Collections;

public class Money : MonoBehaviour {

    public float money = 1f;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Wallet>().Money += money;
            Destroy(gameObject);
        }
    }

}
