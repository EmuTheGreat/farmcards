using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfo : MonoBehaviour
{
    [SerializeField]
    private int objectIndex;
    [SerializeField]
    private ObjectsDatabaseSO dataBase;

    public Image Logo;
    public TextMeshProUGUI Name;

    public void ShowCardInfo()
    {
        var objectData = dataBase.objectsData[objectIndex];
        Logo.sprite = objectData.Sprite;
        Name.text = objectData.Name;
    }

    private void Start()
    {
        ShowCardInfo();
    }
}
