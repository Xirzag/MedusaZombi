using UnityEngine;
using System.Collections;

public class TombHealth : MonoBehaviour {

    public void OnHurt()
    {

    }

    public void OnDead()
    {
        Destroy(gameObject);
    }
}
