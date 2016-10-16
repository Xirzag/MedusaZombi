using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class ZombieCollider : MonoBehaviour {

    private Zombie zombie;

    void Start()
    {
        zombie = transform.parent.GetComponent<Zombie>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.SendMessage("DamageTaken", zombie.biteDamage * Time.deltaTime);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            zombie.bitingPlayer = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            zombie.bitingPlayer = true;
    }

    void DamageTaken(int damage)
    {
        zombie.gameObject.GetComponent<Health>().DamageTaken(damage);
    }
}
