using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Sensor : MonoBehaviour {

    public UnityEvent OnNearPlayer;
    public UnityEvent OnLosePlayer;
    [HideInInspector]
    public GameObject nearPlayer;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nearPlayer = other.gameObject;
            OnNearPlayer.Invoke();
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nearPlayer = other.gameObject;
            OnLosePlayer.Invoke();
        }
    }
}
