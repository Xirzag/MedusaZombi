using UnityEngine;
using System.Collections;

public class CowScript : MovingEntity {

    private FSM fsm;
    private Vector3 targetPosition;
    private Vector3 towardsTarget = Vector3.zero;
    private new Animation animation;

    public float wanderRadius = 5f;

    enum state
    {
        Wander = 0
    }
    
    void Start()
    {
        fsm = new FSM(this, new FSM.StateMethod[] { Wander });
        animation = GetComponent<Animation>();
        animation.clip.wrapMode = WrapMode.Loop;
        animation.Play();

        fsm.Start();
    }

    void RecalculateTargetPosition()
    {
        do
        {
            targetPosition = transform.position + Random.insideUnitSphere * wanderRadius;
            targetPosition.y = 0.5f;
        } while (!TerrainScript.isInsidePlane(targetPosition));
    }

    IEnumerator Wander()
    {

        while (fsm.CurrentState() == (int)state.Wander)
        {
            if (towardsTarget.magnitude < 0.25f)
                RecalculateTargetPosition();

            towardsTarget = targetPosition - transform.position;
            MoveTowards(towardsTarget);

            yield return 0;
        }
    }
}
