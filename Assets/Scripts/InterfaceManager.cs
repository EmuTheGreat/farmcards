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

    private void FixedUpdate()
    {
        balanceText.text = balance.ToString();
        dayText.text = day.ToString();
        waterText.text = water.ToString();
    }
}
