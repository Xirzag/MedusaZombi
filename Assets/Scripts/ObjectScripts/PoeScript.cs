using UnityEngine;
using System.Collections;
using System;

public class PoeScript : MovingEntity {

    private FSM fsm;
    private Vector3 targetPosition;
    private Vector3 towardsTarget = Vector3.zero;
    private Animator animator;

    public float shotRandomness = 20f;
    public float wanderRadius = 5f;
    public GameObject proyectile;
    public float shotCooldown = 2f;

    private float deadAnimationLength = 0.5f;
    
    private GameObject objective;
    private Sensor sensor;


    enum state
    {
        Wander,
        Shooting,
        Dead
    }

    void Start()
    {
        fsm = new FSM(this, new FSM.StateMethod[] { Wander, Shooting, Dead });
        animator = GetComponent<Animator>();
        fsm.Start();

        sensor = transform.FindChild("Sensor").gameObject.GetComponent<Sensor>();
    }

    void RecalculateTargetPosition()
    {
        do
        {
            targetPosition = transform.position + UnityEngine.Random.insideUnitSphere * wanderRadius;
            targetPosition.y = 0.5f;
        } while (!TerrainScript.isInsidePlane(targetPosition));

        Vector3 direction = targetPosition - transform.position;

        animator.SetFloat("y", -direction.z);
        animator.SetFloat("x", direction.x);
        animator.SetBool("moveHorizontal", Mathf.Abs(direction.x) > Mathf.Abs(direction.z));
    }

    IEnumerator Wander()
    {
        while (fsm.CurrentState() == (int)state.Wander)
        {
            if (towardsTarget.magnitude < 0.25f)
                RecalculateTargetPosition();

            towardsTarget = targetPosition - transform.position;
            MoveTowardsWithoutRotate(towardsTarget);
            
            yield return 0;
        }
    }

    IEnumerator Shooting()
    {
        
        float lastShotTime = 0;
        while (fsm.CurrentState() == (int)state.Shooting && objective != null)
        {
            if (Time.time - lastShotTime > shotCooldown) {
                lastShotTime = Time.time;
                float detourAngle = UnityEngine.Random.Range(-shotRandomness / 2, shotRandomness / 2);
                Vector3 shotAngle = Quaternion.LookRotation(objective.transform.position - transform.position).eulerAngles;
                shotAngle.y += detourAngle;
                GameObject.Instantiate(proyectile, transform.position, Quaternion.Euler(shotAngle));
            }

            yield return 0;
        }
    }

    IEnumerator Dead()
    {
        float animationStart = Time.time;
        Vector3 initialScale = transform.localScale;
        while (fsm.CurrentState() == (int)state.Dead && Time.time - animationStart < deadAnimationLength)
        {
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, (Time.time - animationStart) / deadAnimationLength);

            yield return 0;
        }

        Destroy(gameObject);
    }

    public void OnDetectPlayer()
    {
        if (fsm.CurrentState() != (int)state.Dead)
        {
            objective = sensor.nearPlayer;
            fsm.ChangeState((int)state.Shooting);
        }
    }
    
    public void OnLosePlayer()
    {
        objective = null;
        if (fsm.CurrentState() != (int)state.Dead)
            fsm.ChangeState((int)state.Wander);
    }

    public void OnDead()
    {
        fsm.ChangeState((int)state.Dead);
        CounterUI.Add(1);
    }

    public void shot()
    {
        
    }

}
