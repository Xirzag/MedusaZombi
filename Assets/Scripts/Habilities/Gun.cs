using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour {

    public float shotCooldown;
    public GameObject bulletPrefab;
    public UnityEvent OnBulletsChange;
    public float shotRandomness = 90;

    public int initialBullets = 27;
    public AudioClip shotSound;
    private AudioSource sound;

    private float lastShotTime;
    private int bullets;

    public int Bullets {
        get
        {
            return bullets;
        }
        set
        {
            bullets = value;
            OnBulletsChange.Invoke();
        }
    }

    public void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    public void Start()
    {
        bullets = initialBullets;
    }

    public void shot()
    {
        if (bullets > 0 && Time.time - lastShotTime > shotCooldown)
        {
            Vector3 shotAngle = ShotAngle();
            GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.Euler(shotAngle));
            lastShotTime = Time.time;
            Bullets--;

            PlayShotSound();
        }
    }

    private void PlayShotSound()
    {
        sound.pitch = Random.Range(0.9f, 1.1f);
        sound.PlayOneShot(shotSound);
    }

    private Vector3 ShotAngle()
    {
        float detourAngle = Random.Range(-shotRandomness / 2, shotRandomness / 2);
        Vector3 shotAngle = transform.rotation.eulerAngles;
        shotAngle.y += detourAngle;
        return shotAngle;
    }
}
