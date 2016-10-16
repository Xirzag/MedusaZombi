using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CounterUI : MonoBehaviour
{

    static CounterUI main;

    public void Awake()
    {
        main = this;
    }

    Text moneyText;

    void Start()
    {
        moneyText = transform.FindChild("Text").gameObject.GetComponent<Text>();
        Wallet.OnMoneyEarn += SetText;
    }

    public void SetText(float amount)
    {
        moneyText.text = amount.ToString();
    }

    private float counter;

    public static void Add(float amount = 1)
    {
        main.counter += amount;
        main.SetText(main.counter);
    }

}
