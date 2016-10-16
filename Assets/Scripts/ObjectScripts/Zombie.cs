using UnityEngine;
using System.Collections;


public class Zombie : MovingEntity
{

    protected enum state
    {
        Wander = 0,
        Chase = 1,
        Bite = 2,
        Dead = 3
    }

    private Vector3 targetPosition;
    private Vector3 towardsTarget = Vector3.zero;
    private GameObject model;

    public float wanderRadius = 5f;
    public float biteDamage = 10;

    private bool playingAnimation = false;
    public float stepSpeed = 4f;
    public float stepLong = 0.3f;
    public float chaseSpeedUp = 2.75f;

    public float maxChaseDistance;
    private GameObject player;

    private GameObject[] eyes = new GameObject[2];
    private Sensor sensor;

    Vector3 initialScale;
    [HideInInspector]
    public bool bitingPlayer = false;
    public float hurtSizeIncrement = 1.25f;

    public GameObject zombieExplosion;
    public GameObject bloodAnimation;
    public GameObject attackEffectPrefab;

    internal FSM fsm;

    MultipleScale modelScale;
    private int hurtScaleIndex;
    private int moveScaleIndex;

    private SoundManager sound;

    // Use this for initialization
    protected void Awake()
    {
        sound = GetComponent<SoundManager>();
    }


    protected void Start () {
        model = transform.FindChild("ModelZombi").gameObject;
        sensor = transform.FindChild("ZombieSensor").gameObject.GetComponent<Sensor>();
        eyes[0] = model.transform.FindChild("EyeLeft").gameObject;
        eyes[1] = model.transform.FindChild("EyeRight").gameObject;

        modelScale = model.GetComponent<MultipleScale>();
        moveScaleIndex = model.GetComponent<MultipleScale>().NewScale(Vector3.one);
        hurtScaleIndex = model.GetComponent<MultipleScale>().NewScale(model.transform.localScale);

        initialScale = model.transform.localScale;

        fsm = new FSM(this, new FSM.StateMethod[] { Wander, Chase, Bite, Dead });

        fsm.Start();
    }
	

    public void OnNearPLayer()
    {
        Vector3 towardsTarget = sensor.nearPlayer.transform.position - transform.position;

        if (towardsTarget.magnitude > maxChaseDistance)
            return;
        player = sensor.nearPlayer;
        if(player != null)
            fsm.ChangeState((int)state.Chase);
    }


    IEnumerator Wander()
    {

        sound.Play(0);

        foreach (GameObject eye in eyes)
        {
            eye.GetComponent<MeshRenderer>().material.color = Color.white;
            eye.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.black);
        }

        while (fsm.CurrentState() == (int)state.Wander)
        {

            if (towardsTarget.magnitude < 0.25f)
                RecalculateTargetPosition();

            towardsTarget = targetPosition - transform.position;
            MoveTowards(towardsTarget);
            modelScale[moveScaleIndex] = new Vector3(initialScale.x, initialScale.y, initialScale.z + (Mathf.Sin(Time.time * stepSpeed) + 1) * stepLong);

            Debug.DrawLine(transform.position, targetPosition, Color.blue);

            yield return 0;
        }
    }

    IEnumerator Chase()
    {
        sound.Play("Chase");

        foreach (GameObject eye in eyes)
        {
            eye.GetComponent<MeshRenderer>().material.color = Color.red;
            eye.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);
        }

        movementSpeed *= chaseSpeedUp;

        

        while (fsm.CurrentState() == (int)state.Chase && player != null)
        {
            towardsTarget = player.transform.position - transform.position;
            MoveTowards(towardsTarget);
            modelScale[moveScaleIndex] = new Vector3(initialScale.x, initialScale.y, initialScale.z + (Mathf.Sin(Time.time * stepSpeed * chaseSpeedUp) + 1) * stepLong);

            yield return 0;


            if (towardsTarget.magnitude > maxChaseDistance)
                fsm.ChangeState((int)state.Wander);
                

            if(bitingPlayer)
                fsm.ChangeState((int)state.Bite);

        }

        if(player == null)
            fsm.ChangeState((int)state.Wander);

        movementSpeed /= chaseSpeedUp;
    }

    IEnumerator Bite()
    {
        GameObject attackEffect = (GameObject)Instantiate(attackEffectPrefab,
            Vector3.Lerp(transform.position, player.transform.position, 0.5f) + attackEffectPrefab.transform.localPosition,
            attackEffectPrefab.transform.rotation);

        while (fsm.CurrentState() == (int)state.Bite && bitingPlayer && player != null)
        {  
            yield return 0;
        }
        attackEffect.GetComponent<ParticleSystem>().Stop();

        if (!bitingPlayer)
            fsm.ChangeState( (int)state.Chase );

        if (player == null)
            fsm.ChangeState((int)state.Wander);
    }
    
    IEnumerator Dead()
    {

        if(bigEnoughToExplode())
        {
            Instantiate(zombieExplosion, transform.position, Quaternion.identity);
        }else
        {
            GetComponent<Animation>().Play("ZombieDead");
            yield return new WaitForSeconds(GetComponent<Animation>().GetClip("ZombieDead").length);
        }
        
        Destroy(gameObject);

        CounterUI.Add(1);

        yield return 0;
    }

    private bool bigEnoughToExplode()
    {
        float maxSize = (GetComponent<Health>().maxHealth * hurtSizeIncrement * initialScale).magnitude;
        return modelScale[hurtScaleIndex].magnitude > maxSize / 3.5f;
    }

    void RecalculateTargetPosition()
    {
        do
        {
            targetPosition = transform.position + Random.insideUnitSphere * wanderRadius;
            targetPosition.y = 0.5f;
        } while (!TerrainScript.isInsidePlane(targetPosition));
    }

    IEnumerator DamageAnimation()
    {
        Vector3 intialSize = modelScale[hurtScaleIndex];
        float magnitudThreshold = modelScale[hurtScaleIndex].magnitude;

        playingAnimation = true;

        yield return null;

        while (magnitudThreshold < modelScale[hurtScaleIndex].magnitude)
        {
            modelScale[hurtScaleIndex] = Vector3.Lerp(modelScale[hurtScaleIndex], intialSize, Time.deltaTime/2f);
            yield return null;
        }
        modelScale[hurtScaleIndex] = intialSize;
        playingAnimation = false;
    }

    public void OnDamageHandler()
    {
        if (!playingAnimation)
            StartCoroutine(DamageAnimation());

        modelScale[hurtScaleIndex] *= hurtSizeIncrement;
        Instantiate(bloodAnimation, transform.position, Quaternion.identity);
    }

    public void OnDeadHandler()
    {
        fsm.ChangeState((int)state.Dead); 
    }
}
