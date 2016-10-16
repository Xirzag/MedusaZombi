using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {

    public GameObject player;
    private Vector3 vectorFromFloor;
    private new Camera camera;
    public Collider ground;

    public float speed = 10f;

    void Start()
    {
        camera = GetComponent<Camera>();
        getDistanceToFloor();

    }

    private void getDistanceToFloor()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        ground.Raycast(ray, out hit, camera.farClipPlane);
        vectorFromFloor = hit.distance * transform.forward * -1;
    }

    void Update()
    {
        if(player != null)
            transform.position = Vector3.Lerp(transform.position, player.transform.position + vectorFromFloor, speed * Time.deltaTime);
    }
}
