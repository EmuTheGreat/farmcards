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
    private TextMeshProUGUI BuildingsCost;
    public int buildingsCost;

    [SerializeField]
    private TextMeshProUGUI PaymentsMessage;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Balance"))
        {
            SetBalance(100);
        }
        else
        {
            Load();
        }
    }

    public void SetWater(int value = 0)
    {
        water += waterSum + value;
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

    public void SetBuildingsCost(int value)
    {
        buildingsCost += value;
        BuildingsCost.text = buildingsCost.ToString();
    }

    public void SetPaymentsMessage(int cost, int day)
    {
        PaymentsMessage.text = $"Заплати {cost}\nчерез {day}\n{Declension(day)}.";
    }
    
    public void Save()
    {
        PlayerPrefs.SetInt("Balance", balance);
        PlayerPrefs.SetInt("Water", water);
        PlayerPrefs.SetInt("Day", day);
        PlayerPrefs.SetInt("WaterSum", waterSum);
        PlayerPrefs.SetInt("BuildingsCost", buildingsCost);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("Balance"))
        {
            balance = PlayerPrefs.GetInt("Balance");
            balanceText.text = balance.ToString();
            water = PlayerPrefs.GetInt("Water");
            waterText.text = water.ToString();
            day = PlayerPrefs.GetInt("Day");
            dayText.text = day.ToString();
            waterSum = PlayerPrefs.GetInt("WaterSum");
            waterSumText.text = waterSum.ToString();
            buildingsCost = PlayerPrefs.GetInt("BuildingsCost");
            BuildingsCost.text = buildingsCost.ToString();
        }
    }

    private string Declension(int day)
    {
        if (day % 10 == 1 && day != 11)
        {
            return "день";
        }

        else if (day % 10 == 2 && day != 12)
        {
            return "дня";
        }
        else
        {
            return "дней";
        }
    }
}
