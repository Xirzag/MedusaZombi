using UnityEngine;
using System.Collections;

public class MoveTerrain : MonoBehaviour {

    public float speed = 1f;
    public string[] mapsToMove = { "_MainTex" };

    void Update()
    {
        foreach(string map in mapsToMove)
            GetComponent<MeshRenderer>().material.SetTextureOffset(map, new Vector2(Time.time * speed, 0));
    }

}
