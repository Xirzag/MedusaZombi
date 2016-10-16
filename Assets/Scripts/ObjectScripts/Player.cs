using UnityEngine;
using System.Collections;
using System;

public class Player : MovingEntity {
    
	private Gun gun;
    private SlowMotion slowMotion;

    void Start()
    {
        
        gun = GetComponentInChildren<Gun>();
        slowMotion = GetComponentInChildren<SlowMotion>();
    }

	void Update () {
        Vector3 desiredDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            desiredDirection += Vector3.left;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            desiredDirection += Vector3.right;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            desiredDirection += Vector3.forward;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            desiredDirection += Vector3.back;

        if (desiredDirection != Vector3.zero)
            MoveTowards(desiredDirection);

        if (Input.GetKey(KeyCode.Space))
            gun.shot();
        if (Input.GetKey(KeyCode.Q))
            slowMotion.cast();
    }

    

    public void OnDamageHandler()
    {

    }

    public void OnDeadHandler()
    {
        Destroy(gameObject);
    }

}
