using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public int balance;
    [SerializeField]
    private TextMeshProUGUI balanceText;

    private void FixedUpdate()
    {
        balanceText.text = balance.ToString();
    }
}
