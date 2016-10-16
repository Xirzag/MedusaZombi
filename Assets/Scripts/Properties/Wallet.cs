using System;
using System.Collections.Generic;
using UnityEngine;

class Wallet : MonoBehaviour
{

    public delegate void _OnMoneyEarn(float money);
    public static event _OnMoneyEarn OnMoneyEarn;

    private float money;
    public float Money
    {
        get
        {
            return money;
        }

        set
        {
            if (OnMoneyEarn != null)
                OnMoneyEarn(value);

            money = value;
        }
    }

}

