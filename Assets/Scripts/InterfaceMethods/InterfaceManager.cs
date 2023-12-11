using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI balanceText;
    public int balance;

    [SerializeField]
    private TextMeshProUGUI dayText;
    public int day;

    [SerializeField]
    private TextMeshProUGUI waterText;
    public int water;
    
    [SerializeField]
    private TextMeshProUGUI waterSumText;
    public int waterSum;
    [SerializeField]

    private void Start()
    {
        if (PlayerPrefs.HasKey("Balance"))
        {
            balanceText.text = balance.ToString();
        }
        else
        {
            SetBalance(100);
        }
    }

    public void SetWater()
    {
        water += waterSum;
        waterText.text = water.ToString();
    }

    public void SetDay(int value)
    {
        day += value;
        dayText.text = day.ToString();
    }

    public void SetBalance(int value)
    {
        balance += value;
        balanceText.text = balance.ToString();
    }

    public void SetWaterSum(int value)
    {
        waterSum = value;
        waterSumText.text = value.ToString();
    }
    

    void OnDisable()
    {
        PlayerPrefs.SetInt("Balance", balance);

    }

    void OnEnable()
    {
        if (PlayerPrefs.HasKey("Balance"))
        {
            balance = PlayerPrefs.GetInt("Balance");
        }
    }

}
