using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterfaceManager : MonoBehaviour, ISaveState
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
        if (!PlayerPrefs.HasKey("Balance"))
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
    

<<<<<<< HEAD
=======

>>>>>>> 9f933247c394193e543c863ba3dbf535e459689b
    public void Save()
    {
        PlayerPrefs.SetInt("Balance", balance);

    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("Balance"))
        {
            balance = PlayerPrefs.GetInt("Balance");
            balanceText.text = balance.ToString();
        }
    }

    //void OnEnable()
    //{
    //    if (PlayerPrefs.HasKey("Balance"))
    //    {
    //        balance = PlayerPrefs.GetInt("Balance");
    //    }
    //}
}
