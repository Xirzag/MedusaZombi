using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public float speed = 15;
    public float flyTime = 3f;
    public float damage = 2f;
    public string applyToTag = "Enemy";

    Rigidbody rb;

	
	void Awake () {
        Destroy(gameObject, flyTime);
    }
	
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.VelocityChange);
    }

    void OnTriggerEnter(Collider other) {
        if ( other.CompareTag(applyToTag) )
        {
            other.SendMessage("DamageTaken", damage);
            Destroy(gameObject);
        }
    }

}
