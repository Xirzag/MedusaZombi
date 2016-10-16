using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    public delegate void _OnDamaged(GameObject go);
    public static event _OnDamaged OnDamaged;

    public float maxHealth;
    public float currentHealth;
    public UnityEvent OnDamageTaken;
    public UnityEvent OnDead;

    public void DamageTaken (float amount)
    {
        currentHealth -= amount;
        OnDamageTaken.Invoke();

        if (currentHealth <= 0)
            OnDead.Invoke();

        if (OnDamaged != null)
            OnDamaged(gameObject);
    }

}
