using UnityEngine;
using System.Collections;

public class BulletCounter : MonoBehaviour {

    UnityEngine.UI.Text text;
    public Gun gun;

    void Start()
    {
        text = GetComponent<UnityEngine.UI.Text>();
        UpdateBullets();
    }

    public void UpdateBullets()
    {
        text.text = gun.Bullets.ToString();
    }

}
