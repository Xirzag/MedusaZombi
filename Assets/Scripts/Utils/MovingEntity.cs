using UnityEngine;
using System.Collections;

public class MovingEntity : MonoBehaviour {

    public float movementSpeed = 1;
    public float rotationSpeed = 1;

    protected void MoveTowards(Vector3 direction)
    {
        MoveTowardsWithoutRotate(direction);

        Quaternion towardsTargetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, towardsTargetRotation, rotationSpeed * Time.deltaTime);
    }

    protected void MoveTowardsWithoutRotate(Vector3 direction)
    {
        Vector3 nextPosition = direction.normalized * movementSpeed * Time.deltaTime;
        if (TerrainScript.isInsidePlane(transform.position + nextPosition))
            transform.position += nextPosition;

    }

}
