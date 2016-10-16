using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
    private Health health;

    void Start ()
    {
        health = GetComponentInParent<Health>();
    }

    public void UpdateHealthBar()
    {
        float width = (float)health.currentHealth / (float)health.maxHealth;
        width = Mathf.Clamp(width, 0, 100);
        transform.localScale = new Vector3(width, 1f, 1f);
    }
}
