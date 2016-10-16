using UnityEngine;
using System.Collections;
using System;

public class ExplodeOnFloor : MonoBehaviour {

    public GameObject explodePrefab;

    void Update()
    {
        if (transform.position.y < 0 || !TerrainScript.isInsidePlane(transform.position))
            explode();
    }

    private void explode()
    {
        Instantiate(explodePrefab, transform.position, explodePrefab.transform.rotation);
        Destroy(gameObject);
    }
}
