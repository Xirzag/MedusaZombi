using UnityEngine;
using System.Collections;
using System;

class BombZombie : Zombie
{
    public float bombCooldown = 4f;
    public float shotRandomness = 50;
    public float collisionTime = 1f;
    public GameObject bombPrefab = null;
    
    private bool inRangeOfThrowBomb = false;
    private Sensor bombSensor;


    new void Start()
    {
        bombSensor = transform.FindChild("Sensor").gameObject.GetComponent<Sensor>();

        base.Start();
        StartCoroutine(ThrowBombCoroutine());
    }

    public void OnPlayerInRangeOfThrowBomb()
    {
        inRangeOfThrowBomb = true;
    }
    
    public void OnPlayerOutsideRange()
    {
        inRangeOfThrowBomb = false;
    }

    private IEnumerator ThrowBombCoroutine()
    {
        while (base.fsm.CurrentState() != (int) state.Dead )
        {

            yield return new WaitForSeconds(bombCooldown);
            yield return new WaitUntil(() => inRangeOfThrowBomb || base.fsm.CurrentState() != (int)state.Dead);

            if (inRangeOfThrowBomb)
            {
                if (bombSensor.nearPlayer == null)
                    inRangeOfThrowBomb = false;
                else
                    ThrowBomb();
            }

        }
    }

    private void ThrowBomb()
    {
        float detourAngle = UnityEngine.Random.Range(-shotRandomness / 2, shotRandomness / 2);

        Transform player = bombSensor.nearPlayer.transform;

        Vector3 velocity = getVelocity(transform.position,
            player.position +
            player.forward * player.GetComponent<Player>().movementSpeed * collisionTime * 0.75f,
            collisionTime);

        Vector3 shotAngle = Quaternion.LookRotation(velocity).eulerAngles;
        shotAngle.y += detourAngle;

        GameObject bullet = (GameObject)Instantiate(bombPrefab, transform.position, Quaternion.Euler(shotAngle));
        bullet.GetComponent<Bullet>().speed = velocity.magnitude;
    }

    private Vector3 getVelocity(Vector3 initPosition, Vector3 objective, float time)
    {
        Vector3 gravity = Physics.gravity;
        Vector3 vel = (objective - initPosition - gravity*Mathf.Pow(time, 2)/2f);
        return vel / time;
    }

}

