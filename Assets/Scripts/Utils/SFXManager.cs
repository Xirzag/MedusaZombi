using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour {

    void OnEnable ()
    {
        Health.OnDamaged += HandleOnDamaged;
    }

    void OnDisable()
    {
        Health.OnDamaged -= HandleOnDamaged;
    }
    
    void HandleOnDamaged(GameObject go)
    {
        //Debug.LogWarning(go.name + "damaged");
    }

}
