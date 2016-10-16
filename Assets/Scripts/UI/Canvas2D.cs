using UnityEngine;
using System.Collections;

public class Canvas2D : MonoBehaviour {

    Camera cameraToFace;

    void Start()
    {
        cameraToFace = Camera.main;
    }

    void Update()
    {
        transform.rotation = cameraToFace.transform.rotation;
    }

}
