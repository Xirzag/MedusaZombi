using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverScreen : MonoBehaviour {

    private Color backgroundColor;

    public float fadeInTime = 3.5f;

    void Awake()
    {
        backgroundColor = gameObject.GetComponent<Image>().color;
        HideGameOver();
    }

    public void HideGameOver()
    {
        gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);

        for(int i = 0; i < gameObject.transform.childCount; i++)
            gameObject.transform.GetChild(i).gameObject.SetActive(false);


    }

    public void PlayOnGameOver()
    {

        StartCoroutine(GameOverAnimation());

    }

    IEnumerator GameOverAnimation()
    {

        for (int i = 0; i < gameObject.transform.childCount; i++)
            gameObject.transform.GetChild(i).gameObject.SetActive(true);

        float time = 0;

        while (time < fadeInTime) {
            gameObject.GetComponent<Image>().color = 
                Color.Lerp(gameObject.GetComponent<Image>().color, backgroundColor, time/fadeInTime);

            time += Time.deltaTime;

            yield return 0;
        }

        gameObject.GetComponent<Image>().color = backgroundColor;

    }

}
