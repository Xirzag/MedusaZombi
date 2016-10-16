using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PointsUI : MonoBehaviour {

    static Text moneyText;

    void Start()
    {
        moneyText = transform.FindChild("Text").gameObject.GetComponent<Text>();
        Wallet.OnMoneyEarn += SetText;
    }
    
    public void SetText(float amount)
    {
        moneyText.text = amount.ToString();
    }

}
