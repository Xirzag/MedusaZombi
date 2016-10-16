using UnityEngine;
using System.Collections;

public class TerrainScript : MonoBehaviour {

    static private GameObject groundPlane;

    void Start()
    {
        groundPlane = transform.FindChild("Ground").gameObject;
        if (groundPlane == null)
            Debug.LogError("No hay un objeto llamado Ground");
    }


    static public bool isInsidePlane(Vector3 pos)
    {
        if (groundPlane == null)
            return true;

        return groundPlane.GetComponent<MeshRenderer>().bounds.Contains(new Vector3(pos.x, 0, pos.z));
    }

}
