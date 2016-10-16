using UnityEngine;
using System.Collections;

public class ToPosition : MonoBehaviour {

    public GameObject objectToFollow;
    public Vector3 offset;

    void Update()
    {
        transform.position = objectToFollow.transform.position + offset;
    }
}
