using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    public static int Money;
    public int startMoney;

    public static int Lifes;
    public int startLifes;

    public Text moneyUI;
    public Text lifesUI;

    void Awake()
    {
        Money = startMoney;
        Lifes = startLifes;
    }

    
    void FixedUpdate()
    {
        lifesUI.text = "Жизни: " + Lifes;
        moneyUI.text = "Золото: " + Money; 
    }
}
